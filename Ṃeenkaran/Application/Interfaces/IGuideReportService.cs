using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Reports;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuideReportService
    {
        Task<ApiResponse<GuideAnalyticsDto>> GetMyAnalyticsAsync(int guideId);
        Task<ApiResponse<List<GuideFeedbackDto>>> GetMyFeedbacksAsync(int guideId);
        Task<ApiResponse<GuideFeedbackDto>> CreateFeedbackAsync(CreateGuideFeedbackDto dto, int userId);
    }
}
