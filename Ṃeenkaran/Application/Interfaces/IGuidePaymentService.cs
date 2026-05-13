using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Payments;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuidePaymentService
    {
        Task<ApiResponse<CreateGuidePaymentOrderResponseDto>> CreateOrderAsync(int userId, CreateGuidePaymentOrderRequestDto dto);
        Task<ApiResponse<string>> VerifyPaymentAsync(int userId, VerifyGuidePaymentRequestDto dto);
        Task<ApiResponse<List<GuidePaymentDto>>> GetUserPaymentsAsync(int userId);
    }
}
