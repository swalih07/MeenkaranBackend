using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Ṃeenkaran.Infrastructure.Data;

namespace Ṃeenkaran.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly CloudinaryService _cloudinary;
        private readonly JwtService _jwt;

        public AuthService(
            AppDbContext context,
            CloudinaryService cloudinary,
            JwtService jwt)
        {
            _context = context;
            _cloudinary = cloudinary;
            _jwt = jwt;
        }

        // Register
        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Email already exists",
                    StatusCode = 400
                };
            }

            var imageUrl = await _cloudinary.UploadImageAsync(dto.ProfileImage);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                ProfileImageUrl = imageUrl
            };

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "User registered successfully",
                StatusCode = 200
            };
        }


        // Login
        public async Task<ApiResponse<object>> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

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

            // Generate Access Token
            var accessToken = _jwt.GenerateToken(user);

            // Generate Refresh Token
            var refreshToken = _jwt.GenerateRefreshToken();

            // Save Refresh Token
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
    }
}