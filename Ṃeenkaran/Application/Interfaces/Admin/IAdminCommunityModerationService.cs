using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Admin;

namespace Ṃeenkaran.Application.Interfaces.Admin
{
    public interface IAdminCommunityModerationService
    {
        Task<ApiResponse<List<ReportedCommunityPostDto>>> GetReportedPostAsync();
        Task<ApiResponse<string>> RemovePostAsync(int adminId, int postId, RemoveCommunityPostDto dto);


        Task<ApiResponse<List<SuspiciousReviewDto>>> GetSuspiciousReviewAsync();
        Task<ApiResponse<string>> HideReviewAsync(int adminId, int reviewId, HideReviewDto dto);
    }
}
