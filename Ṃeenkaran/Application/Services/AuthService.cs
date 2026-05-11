using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Ṃeenkaran.Domain.Constants;

namespace Ṃeenkaran.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly CloudinaryService _cloudinary;
        private readonly JwtService _jwt;
        private readonly EmailService _email;

        public AuthService(
            AppDbContext context,
            CloudinaryService cloudinary,
            JwtService jwt,
            EmailService email)
        {
            _context = context;
            _cloudinary = cloudinary;
            _jwt = jwt;
            _email = email;
        }

        // Register
        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto dto)
        {
            dto.Email = dto.Email.Trim().ToLower();

            if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Email already exists",
                    StatusCode = 400
                };
            }

            var imageUrl = dto.ProfileImage != null
                ? await _cloudinary.UploadImageAsync(dto.ProfileImage)
                : string.Empty;

            var otp = Random.Shared.Next(100000, 999999).ToString();

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role=Roles.User,
                ProfileImageUrl = imageUrl,
                IsEmailVerified = false,
                EmailOtp = otp,
                EmailOtpExpiryTime = DateTime.UtcNow.AddMinutes(10),
                IsEmailOtpUsed = false
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await _email.SendEmailAsync(
                user.Email,
                "Meenkaran Email OTP Verification",
                $@"
            <h3>Email Verification OTP</h3>
            <p>Your OTP is: <b>{otp}</b></p>
            <p>This OTP will expire in 10 minutes.</p>
        ");

            return new ApiResponse<string>
            {
                Success = true,
                Message = "User registered successfully. OTP sent to email.",
                StatusCode = 200,
                Data = "OTP Sent"
            };
        }


        // Login
        public async Task<ApiResponse<object>> LoginAsync(LoginDto dto)
        {
            var email = dto.Email.Trim().ToLower();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid credentials",
                    StatusCode = 401
                };
            }

            if (!user.IsEmailVerified)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Please verify your email OTP before login",
                    StatusCode = 403
                };
            }

            var accessToken = _jwt.GenerateToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Login Success",
                StatusCode = 200,
                Data = new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Role,
                    user.ProfileImageUrl
                }
            };
        }


        // Refresh Token
        public async Task<ApiResponse<object>> RefreshTokenAsync(
            RefreshTokenDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.RefreshToken == dto.RefreshToken);

            if (user == null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid Refresh Token",
                    StatusCode = 401
                };
            }

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Refresh Token Expired",
                    StatusCode = 401
                };
            }

            // New Access Token
            var newAccessToken = _jwt.GenerateToken(user);

            // New Refresh Token
            var newRefreshToken = _jwt.GenerateRefreshToken();

            // Update DB
            user.RefreshToken = newRefreshToken;

            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Token Refreshed Successfully",
                StatusCode = 200,

                Data = new
                {
                    AccessToken = newAccessToken,

                    RefreshToken = newRefreshToken
                }
            };
        }
        
        public async Task<string> ForgotPasswordAsync(string email)
        {
            email = email.Trim().ToLower();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email);

            if (user == null)
                return "User not found";

            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();

            user.ResetOtp = otp;
            user.ResetOtpExpiry = DateTime.UtcNow.AddMinutes(5);

            await _context.SaveChangesAsync();

            await _email.SendOtpAsync(email, otp);

            return "OTP sent to email";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var email = dto.Email.Trim().ToLower();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email);

            if (user == null)
                return "User not found";

            if (user.ResetOtp != dto.Otp)
                return "Invalid OTP";

            if (user.ResetOtpExpiry < DateTime.UtcNow)
                return "OTP expired";

            // Hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            // Clear OTP
            user.ResetOtp = null;
            user.ResetOtpExpiry = null;

            await _context.SaveChangesAsync();

            return "Password reset successful";
        }
        public async Task<object> GetProfileAsync(string email)
        {
            var user = await _context.Users
                .Where(x => x.Email == email)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Email,
                    x.ProfileImageUrl
                })
                .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public async Task<string> UpdateProfileAsync(string email, UpdateProfileDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return "User not found";

            user.Name = dto.Name;

            if (dto.ProfileImage != null)
            {
                var imageUrl = await _cloudinary.UploadImageAsync(dto.ProfileImage);
                if (imageUrl != null)
                {
                    user.ProfileImageUrl = imageUrl;
                }
            }

            await _context.SaveChangesAsync();

            return "Profile updated successfully";
        }
        public async Task<ApiResponse<string>> VerifyEmailOtpAsync(UserVerifyOtpDto dto)
        {
            var email = dto.Email.Trim().ToLower();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "User not found",
                    StatusCode = 404
                };
            }

            if (user.IsEmailVerified)
            {
                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email already verified",
                    StatusCode = 200,
                    Data = "Verified"
                };
            }

            if (string.IsNullOrWhiteSpace(user.EmailOtp))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "OTP not generated",
                    StatusCode = 400
                };
            }

            if (user.IsEmailOtpUsed)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "OTP already used",
                    StatusCode = 400
                };
            }

            if (user.EmailOtpExpiryTime == null || user.EmailOtpExpiryTime < DateTime.UtcNow)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "OTP expired",
                    StatusCode = 400
                };
            }

            if (user.EmailOtp != dto.Otp)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid OTP",
                    StatusCode = 400
                };
            }

            user.IsEmailVerified = true;
            user.IsEmailOtpUsed = true;
            user.EmailOtp = null;
            user.EmailOtpExpiryTime = null;

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Email verified successfully",
                StatusCode = 200,
                Data = "Verified"
            };
        }
    }
}