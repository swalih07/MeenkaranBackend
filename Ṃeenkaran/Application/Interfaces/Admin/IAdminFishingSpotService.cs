using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.FishingSpot;

namespace Ṃeenkaran.Application.Interfaces.Admin
{
    public interface IAdminFishingSpotService
    {
        Task<ApiResponse<List<FishingSpotDto>>> GetPendingSpotRequestsAsync();
        Task<ApiResponse<string>> ApproveSpotAsync(int adminId, int spotId);
        Task<ApiResponse<string>> RejectSpotAsync(int adminId, int spotId, RejectFishingSpotDto dto);
        Task<ApiResponse<string>> RemoveSpotAsync(int adminId, int spotId);
    }
}
