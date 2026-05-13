using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Payments;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuideEarningService
    {
        Task<ApiResponse<List<GuidePaymentDto>>> GetMyPaymentsAsync(int guideId);
        Task<ApiResponse<GuideEarningsSummaryDto>> GetMySummaryAsync(int guideId);
        Task<ApiResponse<GuidePaymentDto>> GetPaymentByIdAsync(int guideId, int paymentId);
    }
}
