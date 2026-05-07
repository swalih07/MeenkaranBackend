using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface ILeaderboardService
    {
        Task<List<LeaderboardDto>> GetTopUserAsync();
    }
}
