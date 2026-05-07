using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface ICatchPostService
    {
        Task<string>CreatePostAsync(CreateCatchPostDto dto);
        Task<List<CatchFeedDto>> GetCommunityFeedAsync(string location);
    }
}
