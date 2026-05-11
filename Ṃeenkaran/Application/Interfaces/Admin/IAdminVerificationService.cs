using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Admin;

namespace Ṃeenkaran.Application.Interfaces.Admin
{
    public interface IAdminVerificationService
    {
        Task<ApiResponse<List<GuideVerificationDto>>> GetPendingGuidesAsync();
        Task<ApiResponse<string>> VerifyGuideAsync(int guideId);
        Task<ApiResponse<string>> RejectGuideAsync(int guideId, RejectedGuideDto dto);
        Task<ApiResponse<string>> BlockUserAsync(int userId, BlockAccountDto dto);
        Task<ApiResponse<string>> BlockGuideAsync(int guideId, BlockAccountDto dto);
    }
}
