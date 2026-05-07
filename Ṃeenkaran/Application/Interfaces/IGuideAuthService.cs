using CloudinaryDotNet;
using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuideAuthService
    {
        Task<ApiResponse<string>> RegisterAsync(GuideRegisterDto dto);
        Task<ApiResponse<AuthTokenDto>> LoginAsync(GuideLoginDto dto);
        Task<ApiResponse<AuthTokenDto>> RefreshTokenAsync(RefreshTokenRequestDto dto);


        Task<ApiResponse<string>> ForgotPasswordAsync(GuideForgetPasseordDto dto);
        Task<ApiResponse<string>> VerifyOtpAsync(GuideVerifyOtpDto dto);
        Task<ApiResponse<string>> ResetPasswordAsync(GuideResetPasswordDto dto);
    }
}
