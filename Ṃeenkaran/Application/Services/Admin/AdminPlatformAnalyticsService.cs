using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Financial;
using Ṃeenkaran.Domain.Entities.Admin;
using Ṃeenkaran.Domain.Enums;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ṃeenkaran.Application.Interfaces.Admin;

namespace Ṃeenkaran.Application.Services.Admin
{
    public class AdminPlatformAnalyticsService : IAdminPlatformAnalyticsService
    {
        private readonly AppDbContext _context;

        public AdminPlatformAnalyticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<PlatformOverviewDto>> GetOverviewAsync()
        {
            var activeUsers = await _context.Users
                .CountAsync(x => x.Role == "User" && !x.IsBlocked);

            var activeGuides = await _context.Users
                .CountAsync(x => x.Role == "Guide" && !x.IsBlocked);

            var successfulTrips = await _context.TripBookings
                .CountAsync(x => x.BookingStatus == BookingStatus.Completed);

            var totalCommissionEarned = await _context.TripBookings
                .Where(x => x.BookingStatus == BookingStatus.Completed)
                .SumAsync(x => (decimal?)x.CommissionAmount) ?? 0m;

            var pendingGuidePayouts = await _context.GuidePayouts
                .Where(x => x.Status == PayoutStatus.Pending || x.Status == PayoutStatus.Ready)
                .SumAsync(x => (decimal?)x.PayoutAmount) ?? 0m;

            var sentGuidePayouts = await _context.GuidePayouts
                .Where(x => x.Status == PayoutStatus.Sent)
                .SumAsync(x => (decimal?)x.PayoutAmount) ?? 0m;

            var dto = new PlatformOverviewDto
            {
                ActiveUsers = activeUsers,
                ActiveGuides = activeGuides,
                SuccessfulTrips = successfulTrips,
                TotalCommissionEarned = totalCommissionEarned,
                PendingGuidePayouts = pendingGuidePayouts,
                SentGuidePayouts = sentGuidePayouts
            };

            return ApiResponse<PlatformOverviewDto>.SuccessResponse(
                dto,
                "Platform overview fetched successfully.",
                200
            );
        }

        public async Task<ApiResponse<List<CommissionReportDto>>> GetCommissionReportAsync(DateTime? from = null, DateTime? to = null)
        {
            var query = _context.TripBookings
                .AsNoTracking()
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(x => x.CreatedAt >= from.Value);

            if (to.HasValue)
                query = query.Where(x => x.CreatedAt <= to.Value);

            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new CommissionReportDto
                {
                    TripBookingId = x.Id,
                    GuideId = x.GuideId,
                    UserId = x.UserId,
                    GuideAmount = x.GuideAmount,
                    CommissionPercent = x.CommissionPercent,
                    CommissionAmount = x.CommissionAmount,
                    UserServiceCharge = x.UserServiceCharge,
                    TotalUserPays = x.TotalUserPays,
                    NetGuideEarnings = x.NetGuideEarnings,
                    BookingStatus = x.BookingStatus.ToString(),
                    IsPayoutReleased = x.IsPayoutReleased,
                    CreatedAt = x.CreatedAt,
                    CompletedAt = x.CompletedAt
                })
                .ToListAsync();

            return ApiResponse<List<CommissionReportDto>>.SuccessResponse(
                data,
                "Commission report fetched successfully.",
                200
            );
        }

        public async Task<ApiResponse<List<GuidePayoutDto>>> GetPayoutsAsync(int? guideId = null, string? status = null)
        {
            var query = _context.GuidePayouts
                .AsNoTracking()
                .AsQueryable();

            if (guideId.HasValue)
                query = query.Where(x => x.GuideId == guideId.Value);

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<PayoutStatus>(status, true, out var parsedStatus))
                query = query.Where(x => x.Status == parsedStatus);

            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new GuidePayoutDto
                {
                    Id = x.Id,
                    TripBookingId = x.TripBookingId,
                    GuideId = x.GuideId,
                    PayoutAmount = x.PayoutAmount,
                    Status = x.Status.ToString(),
                    ReleasedByAdminId = x.ReleasedByAdminId,
                    ExternalReference = x.ExternalReference,
                    ReleasedAt = x.ReleasedAt,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return ApiResponse<List<GuidePayoutDto>>.SuccessResponse(
                data,
                "Guide payouts fetched successfully.",
                200
            );
        }

        public async Task<ApiResponse<GuidePayoutDto>> ReleasePayoutAsync(int tripBookingId, int adminId, string? externalReference)
        {
            var booking = await _context.TripBookings
                .FirstOrDefaultAsync(x => x.Id == tripBookingId);

            if (booking == null)
                return ApiResponse<GuidePayoutDto>.FailResponse("Trip booking not found.", 404);

            if (booking.BookingStatus != BookingStatus.Completed)
                return ApiResponse<GuidePayoutDto>.FailResponse("Only completed trips can be settled.", 400);

            if (booking.IsPayoutReleased)
                return ApiResponse<GuidePayoutDto>.FailResponse("Payout already released for this booking.", 400);

            var payout = await _context.GuidePayouts
                .FirstOrDefaultAsync(x => x.TripBookingId == tripBookingId);

            if (payout == null)
            {
                payout = new GuidePayout
                {
                    TripBookingId = booking.Id,
                    GuideId = booking.GuideId,
                    PayoutAmount = booking.NetGuideEarnings,
                    Status = PayoutStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };

                _context.GuidePayouts.Add(payout);
                await _context.SaveChangesAsync();
            }

            payout.Status = PayoutStatus.Sent;
            payout.ReleasedByAdminId = adminId;
            payout.ExternalReference = externalReference;
            payout.ReleasedAt = DateTime.UtcNow;

            booking.IsPayoutReleased = true;
            booking.PayoutReleasedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var dto = new GuidePayoutDto
            {
                Id = payout.Id,
                TripBookingId = payout.TripBookingId,
                GuideId = payout.GuideId,
                PayoutAmount = payout.PayoutAmount,
                Status = payout.Status.ToString(),
                ReleasedByAdminId = payout.ReleasedByAdminId,
                ExternalReference = payout.ExternalReference,
                ReleasedAt = payout.ReleasedAt,
                CreatedAt = payout.CreatedAt
            };

            return ApiResponse<GuidePayoutDto>.SuccessResponse(
                dto,
                "Payout released successfully.",
                200
            );
        }
    }
}
