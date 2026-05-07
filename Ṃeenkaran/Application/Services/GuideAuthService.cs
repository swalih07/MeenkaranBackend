using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class GuideAuthService : IGuideAuthService
    {
        private readonly AppDbContext _context;
        private readonly CloudinaryService _cloudinaryService;
        private readonly JwtService _jwt;
        private readonly EmailService _emailService;

        public GuideAuthService(
            AppDbContext context,
            CloudinaryService cloudinaryService,
            JwtService jwt,
            EmailService emailService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
            _jwt = jwt;
            _emailService = emailService;
        }

        public async Task<ApiResponse<string>> RegisterAsync(GuideRegisterDto dto)
        {
            var emailExists = await _context.Guides.AnyAsync(x => x.Email == dto.Email);
            if (emailExists)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Email already exists",
                    StatusCode = 400
                };
            }

            var phoneExists = await _context.Guides.AnyAsync(x => x.PhoneNumber == dto.PhoneNumber);
            if (phoneExists)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Phone number already exists",
                    StatusCode = 400
                };
            }

            var profileUrl = await _cloudinaryService.UploadImageAsync(dto.ProfileImage);
            var idProofUrl = await _cloudinaryService.UploadImageAsync(dto.IdProof);

            var guide = new Guide
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                ProfileImageUrl = profileUrl,
                IdProofUrl = idProofUrl,
                Area = dto.Area,
                FishingStyle = dto.FishingStyle,
                ExperienceYears = dto.ExperienceYears,
                PricePerDay = dto.PricePerDay,
                IsApproved = false,
                IsAvailable = true,
                Rating = 0
            };

            _context.Guides.Add(guide);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Guide registration successful. Please wait for 24 hours for profile verification",
                StatusCode = 200,
                Data = "Pending Approval"
            };
        }

        public async Task<ApiResponse<AuthTokenDto>> LoginAsync(GuideLoginDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (guide == null)
            {
                return new ApiResponse<AuthTokenDto>
                {
                    Success = false,
                    Message = "Invalid email or password",
                    StatusCode = 401
                };
            }

            if (string.IsNullOrWhiteSpace(guide.PasswordHash))
            {
                return new ApiResponse<AuthTokenDto>
                {
                    Success = false,
                    Message = "Password not found. Please register again.",
                    StatusCode = 400
                };
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, guide.PasswordHash);
            if (!isValidPassword)
            {
                return new ApiResponse<AuthTokenDto>
                {
                    Success = false,
                    Message = "Invalid email or password",
                    StatusCode = 401
                };
            }

            if (!guide.IsApproved)
            {
                return new ApiResponse<AuthTokenDto>
                {
                    Success = false,
                    Message = "Your guide profile is under review. Please wait for 24 hours",
                    StatusCode = 403
                };
            }

            var accessToken = _jwt.GenerateToken(guide.Id, guide.Name, guide.Email, "Guide");
            var refreshToken = _jwt.GenerateRefreshToken();

            guide.RefreshToken = refreshToken;
            guide.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return new ApiResponse<AuthTokenDto>
            {
                Success = true,
                Message = "Guide login successful",
                StatusCode = 200,
                Data = new AuthTokenDto
                {
                    Id = guide.Id,
                    Name = guide.Name,
                    Email = guide.Email,
                    Role = "Guide",
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = guide.RefreshTokenExpiryTime.Value
                }
            };
        }

        public async Task<ApiResponse<AuthTokenDto>> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.RefreshToken == dto.RefreshToken);

            if (guide == null)
            {
                return new ApiResponse<AuthTokenDto>
                {
                    Success = false,
                    Message = "Invalid refresh token",
                    StatusCode = 401
                };
            }

            if (guide.RefreshTokenExpiryTime == null || guide.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new ApiResponse<AuthTokenDto>
                {
                    Success = false,
                    Message = "Refresh token expired",
                    StatusCode = 401
                };
            }

            var newAccessToken = _jwt.GenerateToken(guide.Id, guide.Name, guide.Email, "Guide");
            var newRefreshToken = _jwt.GenerateRefreshToken();

            guide.RefreshToken = newRefreshToken;
            guide.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return new ApiResponse<AuthTokenDto>
            {
                Success = true,
                Message = "Token refreshed successfully",
                StatusCode = 200,
                Data = new AuthTokenDto
                {
                    Id = guide.Id,
                    Name = guide.Name,
                    Email = guide.Email,
                    Role = "Guide",
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    RefreshTokenExpiryTime = guide.RefreshTokenExpiryTime.Value
                }
            };
        }

        public async Task<ApiResponse<string>> ForgotPasswordAsync(GuideForgetPasseordDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (guide == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Guide not found",
                    StatusCode = 404
                };
            }

            var otp = Random.Shared.Next(100000, 1000000).ToString();

            guide.PasswordResetOtp = otp;
            guide.PasswordResetOtpExpiryTime = DateTime.UtcNow.AddMinutes(10);
            guide.IsPasswordResetOtpUsed = false;

            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(
                guide.Email,
                "Meenkaran Guide Password Reset OTP",
                $@"
                    <h3>Password Reset OTP</h3>
                    <p>Your OTP is: <b>{otp}</b></p>
                    <p>This OTP will expire in 10 minutes</p>
                ");

            return new ApiResponse<string>
            {
                Success = true,
                Message = "OTP sent to your email",
                StatusCode = 200,
                Data = "OTP Sent"
            };
        }

        public async Task<ApiResponse<string>> VerifyOtpAsync(GuideVerifyOtpDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (guide == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Guide not found",
                    StatusCode = 404
                };
            }

            if (string.IsNullOrWhiteSpace(guide.PasswordResetOtp))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "OTP not generated",
                    StatusCode = 400
                };
            }

            if (guide.IsPasswordResetOtpUsed)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "OTP already used",
                    StatusCode = 400
                };
            }

            if (guide.PasswordResetOtpExpiryTime == null || guide.PasswordResetOtpExpiryTime < DateTime.UtcNow)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "OTP expired",
                    StatusCode = 400
                };
            }

            if (guide.PasswordResetOtp != dto.Otp)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid OTP",
                    StatusCode = 400
                };
            }

            guide.IsPasswordResetOtpUsed = true;
            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "OTP verified successfully",
                StatusCode = 200,
                Data = "Verified"
            };
        }

        public async Task<ApiResponse<string>> ResetPasswordAsync(GuideResetPasswordDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (guide == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Guide not found",
                    StatusCode = 404
                };
            }

            if (string.IsNullOrWhiteSpace(guide.PasswordResetOtp))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "OTP not generated",
                    StatusCode = 400
                };
            }

            if (!guide.IsPasswordResetOtpUsed)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "OTP not verified",
                    StatusCode = 400
                };
            }

            if (guide.PasswordResetOtp != dto.Otp)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid OTP",
                    StatusCode = 400
                };
            }

            guide.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            guide.PasswordResetOtp = null;
            guide.PasswordResetOtpExpiryTime = null;
            guide.IsPasswordResetOtpUsed = false;

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Password reset successful",
                StatusCode = 200,
                Data = "Password Updated"
            };
        }
    }
}