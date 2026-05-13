using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Payments;

namespace Ṃeenkaran.Application.Interfaces.Admin
{
    public interface IAdminPaymentService
    {
        Task<ApiResponse<PlatformPaymentSettingDto>> GetSettingsAsync();
        Task<ApiResponse<PlatformPaymentSettingDto>> UpdateSettingsAsync(int adminId, UpdatePlatformPaymentSettingRequestDto request);

        Task<ApiResponse<List<GuidePaymentDto>>> GetAllPaymentsAsync();
        Task<ApiResponse<GuidePaymentDto>> GetPaymentByIdAsync(int paymentId);

        Task<ApiResponse<AdminPaymentReportDto>> GetReportAsync();
    }
}
