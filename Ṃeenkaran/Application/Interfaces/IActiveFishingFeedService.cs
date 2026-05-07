using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IActiveFishingFeedService
    {
        Task<List<ActiveFishingFeedDto>> GetLiveFeedsAsync();
    }
}
