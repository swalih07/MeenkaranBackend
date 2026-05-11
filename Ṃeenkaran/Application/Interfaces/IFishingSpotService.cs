using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.FishingSpot;
using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IFishingSpotService
    {
        Task<ApiResponse<string>> AddSpotAsync(int guideId, AddFishingSpotDto dto);

        Task<ApiResponse<string>> UpdateSpotAsync(int guideId, int spotId, UpdateFishingSpotDto dto);

        Task<ApiResponse<string>> DeleteSpotAsync(int guideId, int spotId);

        Task<ApiResponse<FishingSpotDto>> GetSpotByIdAsync(int spotId);

        Task<ApiResponse<List<FishingSpotDto>>> GetAllHotspotAsync();

        Task<ApiResponse<List<FishingSpotDto>>> GetGuideSpotsAsync(int guideId);

        Task<ApiResponse<List<NearbyFishingSpotDto>>> GetNearbySpotsAsync(double lat, double lon, double radius = 5);
    }
}
