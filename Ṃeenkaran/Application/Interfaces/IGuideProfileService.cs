using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuideProfileService
    {
        Task<ApiResponse<GuideProfileDto>> GetMyProfileAsync(int guideId);
        Task<ApiResponse<string>> UpdateMyProfileAsync(int guideid, UpdateGuideProfileDto dto);
        Task<ApiResponse<GuideFullProfileDto>> GetGuideProfileAsync(int guideId);
    }
}
