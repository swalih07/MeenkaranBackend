using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IFishingSpotService
    {
        Task<List<FishingSpotDto>> GetNearbySpotsAsync(double lat, double lon, double radius = 5);
    }
}
