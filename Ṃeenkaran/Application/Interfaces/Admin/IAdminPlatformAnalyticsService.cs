using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Financial;

namespace Ṃeenkaran.Application.Interfaces.Admin
{
    public interface IAdminPlatformAnalyticsService
    {
        Task<ApiResponse<PlatformOverviewDto>> GetOverviewAsync();
        Task<ApiResponse<List<CommissionReportDto>>> GetCommissionReportAsync(DateTime? from = null, DateTime? to = null);
        Task<ApiResponse<List<GuidePayoutDto>>> GetPayoutsAsync(int? guideId = null, string? status = null);
        Task<ApiResponse<GuidePayoutDto>> ReleasePayoutAsync(int tripBookingId, int adminId, string? externalReference);
    }
}
