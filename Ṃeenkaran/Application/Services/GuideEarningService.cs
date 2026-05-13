using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Payments;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class GuideEarningService : IGuideEarningService
    {
        private readonly AppDbContext _context;

        public GuideEarningService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<GuidePaymentDto>>> GetMyPaymentsAsync(int guideId)
        {
            var items = await _context.GuidePayments
                .Where(x => x.GuideId == guideId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new GuidePaymentDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    GuideId = x.GuideId,
                    GuideAmount = x.GuideAmount,
                    PlatformFeePercent = x.PlatformFeePercent,
                    PlatformFeeFromGuide = x.PlatformFeeFromGuide,
                    UserServiceCharge = x.UserServiceCharge,
                    TotalUserPays = x.TotalUserPays,
                    GuideReceives = x.GuideReceives,
                    Currency = x.Currency,
                    RazorpayOrderId = x.RazorpayOrderId,
                    RazorpayPaymentId = x.RazorpayPaymentId,
                    RazorpaySignature = x.RazorpaySignature,
                    PaymentStatus = x.PaymentStatus,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    PaidAt = x.PaidAt
                })
                .ToListAsync();

            return ApiResponse<List<GuidePaymentDto>>.SuccessResponse(items, "Guide payments fetched successfully");
        }

        public async Task<ApiResponse<GuideEarningsSummaryDto>> GetMySummaryAsync(int guideId)
        {
            var payments = await _context.GuidePayments
                .Where(x => x.GuideId == guideId)
                .ToListAsync();

            var summary = new GuideEarningsSummaryDto
            {
                GuideId = guideId,
                TotalPayments = payments.Count,
                PaidPayments = payments.Count(x => x.PaymentStatus == "Paid"),
                PendingPayments = payments.Count(x => x.PaymentStatus == "Pending"),
                TotalGuideAmount = payments.Sum(x => x.GuideAmount),
                TotalPlatformFeeFromGuide = payments.Sum(x => x.PlatformFeeFromGuide),
                TotalUserServiceCharge = payments.Sum(x => x.UserServiceCharge),
                TotalGuideReceives = payments.Sum(x => x.GuideReceives),
                PaidGuideReceives = payments.Where(x => x.PaymentStatus == "Paid").Sum(x => x.GuideReceives),
                PendingGuideReceives = payments.Where(x => x.PaymentStatus == "Pending").Sum(x => x.GuideReceives)
            };

            return ApiResponse<GuideEarningsSummaryDto>.SuccessResponse(summary, "Guide earnings summary fetched successfully");
        }

        public async Task<ApiResponse<GuidePaymentDto>> GetPaymentByIdAsync(int guideId, int paymentId)
        {
            var item = await _context.GuidePayments
                .Where(x => x.GuideId == guideId && x.Id == paymentId)
                .Select(x => new GuidePaymentDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    GuideId = x.GuideId,
                    GuideAmount = x.GuideAmount,
                    PlatformFeePercent = x.PlatformFeePercent,
                    PlatformFeeFromGuide = x.PlatformFeeFromGuide,
                    UserServiceCharge = x.UserServiceCharge,
                    TotalUserPays = x.TotalUserPays,
                    GuideReceives = x.GuideReceives,
                    Currency = x.Currency,
                    RazorpayOrderId = x.RazorpayOrderId,
                    RazorpayPaymentId = x.RazorpayPaymentId,
                    RazorpaySignature = x.RazorpaySignature,
                    PaymentStatus = x.PaymentStatus,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    PaidAt = x.PaidAt
                })
                .FirstOrDefaultAsync();

            if (item == null)
                return ApiResponse<GuidePaymentDto>.FailResponse("Payment not found", 404);

            return ApiResponse<GuidePaymentDto>.SuccessResponse(item, "Payment fetched successfully");
        }
    }
}
