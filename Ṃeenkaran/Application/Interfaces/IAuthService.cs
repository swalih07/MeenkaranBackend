using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.User;
using System.Security.Claims;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegisterAsync(RegisterDto dto);
        Task<ApiResponse<object>>LoginAsync(LoginDto dto);
        Task<ApiResponse<object>>RefreshTokenAsync(RefreshTokenDto dto);
        Task<string> ForgotPasswordAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordDto dto);
        Task<object> GetProfileAsync(string email);
        Task<string> UpdateProfileAsync(string email, UpdateProfileDto dto);
    }
}
