using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuideReviewService
    {
        Task<string> AddReviewAsync(int userId, CreateGuideReviewDto dto);
        Task<string> DeleteReviewAsync(int userId, int reviewId);
        Task<List<GuideReviewDto>>GetGuideReviewsAsync(int guideId);
    }
}
