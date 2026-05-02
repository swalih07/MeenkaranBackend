using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegisterAsync(RegisterDto dto);
        Task<ApiResponse<object>>LoginAsync(LoginDto dto);
        Task<ApiResponse<object>>RefreshTokenAsync(RefreshTokenDto dto);
    }
}
