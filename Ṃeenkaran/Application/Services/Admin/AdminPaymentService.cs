using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Payments;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services.Admin
{
    public class AdminPaymentService : IAdminPaymentService
    {
        private readonly AppDbContext _context;

        public AdminPaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<PlatformPaymentSettingDto>> GetSettingsAsync()
        {
            var setting = await GetOrCreateSettingAsync();

            return ApiResponse<PlatformPaymentSettingDto>.SuccessResponse(MapSetting(setting), "Payment settings fetched successfully");
        }

        public async Task<ApiResponse<PlatformPaymentSettingDto>> UpdateSettingsAsync(int adminId, UpdatePlatformPaymentSettingRequestDto request)
        {
            var setting = await GetOrCreateSettingAsync();

            setting.GuidePlatformFeePercent = request.GuidePlatformFeePercent;
            setting.UserServiceCharge = request.UserServiceCharge;
            setting.IsActive = true;
            setting.UpdatedByAdminId = adminId;
            setting.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return ApiResponse<PlatformPaymentSettingDto>.SuccessResponse(MapSetting(setting), "Payment settings updated successfully");
        }

        public async Task<ApiResponse<List<GuidePaymentDto>>> GetAllPaymentsAsync()
        {
            var payments = await _context.GuidePayments
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => MapPayment(x))
                .ToListAsync();

            return ApiResponse<List<GuidePaymentDto>>.SuccessResponse(payments, "All payments fetched successfully");
        }

        public async Task<ApiResponse<GuidePaymentDto>> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await _context.GuidePayments
                .Where(x => x.Id == paymentId)
                .Select(x => MapPayment(x))
                .FirstOrDefaultAsync();

            if (payment == null)
                return ApiResponse<GuidePaymentDto>.FailResponse("Payment not found", 404);

            return ApiResponse<GuidePaymentDto>.SuccessResponse(payment, "Payment fetched successfully");
        }

        public async Task<ApiResponse<AdminPaymentReportDto>> GetReportAsync()
        {
            var payments = await _context.GuidePayments.ToListAsync();

            var report = new AdminPaymentReportDto
            {
                TotalPayments = payments.Count,
                PaidPayments = payments.Count(x => x.PaymentStatus == "Paid"),
                PendingPayments = payments.Count(x => x.PaymentStatus == "Pending"),
                FailedPayments = payments.Count(x => x.PaymentStatus == "Failed"),

                TotalGuideAmount = payments.Sum(x => x.GuideAmount),
                TotalPlatformFeeFromGuide = payments.Sum(x => x.PlatformFeeFromGuide),
                TotalUserServiceCharge = payments.Sum(x => x.UserServiceCharge),
                TotalUserPays = payments.Sum(x => x.TotalUserPays),
                TotalGuideReceives = payments.Sum(x => x.GuideReceives),

                TotalPaidAmount = payments.Where(x => x.PaymentStatus == "Paid").Sum(x => x.TotalUserPays),
                TotalPendingAmount = payments.Where(x => x.PaymentStatus == "Pending").Sum(x => x.TotalUserPays)
            };

            return ApiResponse<AdminPaymentReportDto>.SuccessResponse(report, "Payment report fetched successfully");
        }

        private async Task<PlatformPaymentSetting> GetOrCreateSettingAsync()
        {
            var setting = await _context.PlatformPaymentSettings.FirstOrDefaultAsync(x => x.IsActive);

            if (setting != null)
                return setting;

            setting = new PlatformPaymentSetting
            {
                GuidePlatformFeePercent = 20m,
                UserServiceCharge = 15m,
                IsActive = true,
                UpdatedByAdminId = 0,
                CreatedAt = DateTime.UtcNow
            };

            _context.PlatformPaymentSettings.Add(setting);
            await _context.SaveChangesAsync();

            return setting;
        }

        private static PlatformPaymentSettingDto MapSetting(PlatformPaymentSetting x)
        {
            return new PlatformPaymentSettingDto
            {
                Id = x.Id,
                GuidePlatformFeePercent = x.GuidePlatformFeePercent,
                UserServiceCharge = x.UserServiceCharge,
                IsActive = x.IsActive,
                UpdatedByAdminId = x.UpdatedByAdminId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            };
        }

        private static GuidePaymentDto MapPayment(GuidePayment x)
        {
            return new GuidePaymentDto
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
            };
        }
    }
}
