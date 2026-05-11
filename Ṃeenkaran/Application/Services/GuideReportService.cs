using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Reports;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Enums;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ṃeenkaran.Domain.Entities.User;

namespace Ṃeenkaran.Application.Services
{
    public class GuideReportService:IGuideReportService
    {
        private readonly AppDbContext _context;

        public GuideReportService(AppDbContext context)
        {
            _context= context;
        }

        public async Task<ApiResponse<GuideAnalyticsDto>> GetMyAnalyticsAsync(int guideId)
        {


            var totalTrips = await _context.GuideBookings.CountAsync(x => x.GuideId == guideId);
            var pendingTrips = await _context.GuideBookings.CountAsync(x => x.GuideId == guideId && x.Status == BookingStatus.Pending);
            var acceptedTrips = await _context.GuideBookings.CountAsync(x => x.GuideId == guideId && x.Status == BookingStatus.Accepted);
            var rejectedTrips = await _context.GuideBookings.CountAsync(x => x.GuideId == guideId && x.Status == BookingStatus.Rejected);
            var completedTrips = await _context.GuideBookings.CountAsync(x => x.GuideId == guideId && x.Status == BookingStatus.Completed);
            var cancelledTrips = await _context.GuideBookings.CountAsync(x => x.GuideId == guideId && x.Status == BookingStatus.Cancelled);
            var totalErnings = await _context.GuideBookings
                .Where(x => x.GuideId == guideId && x.Status == BookingStatus.Completed)
                .SumAsync(x => (decimal?)x.TotelAmount) ?? 0m;
            var totalFeedbackCount = await _context.GuideFeedbacks
                .CountAsync(x => x.GuideId == guideId);
            var averageRating = await _context.GuideFeedbacks
                .Where(x => x.GuideId == guideId)
                .AverageAsync(x => (double?)x.Rating) ?? 0;
            var dto = new GuideAnalyticsDto
            {
                GuideId = guideId,
                TotalTrips = totalTrips,
                PendingTrips = pendingTrips,
                AcceptedTrips = acceptedTrips,
                RejectedTrips = rejectedTrips,
                CompletedTrips = completedTrips,
                CancelledTrips = cancelledTrips,
                TotalEarnings = totalErnings,
                AverageRatings = averageRating,
                TotalFeedbackCount = totalFeedbackCount,

            };
            return ApiResponse<GuideAnalyticsDto>.SuccessResponse(dto, "Analytics loaded successfully", 200);
        }
        public async Task<ApiResponse<List<GuideFeedbackDto>>>GetMyFeedbacksAsync(int guideId)
        {
            var guideExists = await _context.Guides.AnyAsync(g => g.Id == guideId);
            if (!guideExists)
            {
                return ApiResponse<List<GuideFeedbackDto>>.FailResponse("Guide not found", 404);
            }
            var feedbacks = await _context.GuideFeedbacks
                .Where(f => f.GuideId == guideId)
                .Include(f => f.User)
                .OrderByDescending(f => f.CreatedAt)
                .Select(f => new GuideFeedbackDto
                {
                    id = f.Id,
                    GuideId = f.GuideId,
                    UserId = f.UserId,
                    UserName = f.User.Name,
                    GuideBookingId = f.GuideBookingId,
                    Rating = f.Rating,
                    Comment = f.Comment,
                    CreatedAt = f.CreatedAt,

                })
                .ToListAsync();
            return ApiResponse<List<GuideFeedbackDto>>.SuccessResponse(feedbacks, "Feedbacks loaded successfully", 200);

        }
        public async Task<ApiResponse<GuideFeedbackDto>>CreateFeedbackAsync(CreateGuideFeedbackDto dto,int userId)
        {
            var booking = await _context.GuideBookings
                .Include(b => b.Guide)
                .FirstOrDefaultAsync(b => b.Id == dto.GuideBookinId);

            if(booking== null)
            {
                return ApiResponse<GuideFeedbackDto>.FailResponse("Booking not found", 404);
            }
            if(booking.UserId != userId)
            {
                return ApiResponse<GuideFeedbackDto>.FailResponse("You cannot review this booking", 403);
            }
            if(booking.Status != BookingStatus.Completed)
            {
                return ApiResponse<GuideFeedbackDto>.FailResponse("You can give feedback only after trip compltion", 400);
            }
            var existingFeedback = await _context.GuideFeedbacks
                .AnyAsync(f => f.GuideBookingId == dto.GuideBookinId);
            if(existingFeedback)
            {
                return ApiResponse<GuideFeedbackDto>.FailResponse("Feedback already submitted for this booking", 409);
            }
            var feedback = new GuideFeedback
            {
                GuideId = booking.GuideId,
                UserId = userId,
                GuideBookingId = dto.GuideBookinId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };
            _context.GuideFeedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            var averageRating = await _context.GuideFeedbacks
                .Where(f => f.GuideId == booking.GuideId)
                .AverageAsync(f => (double?)f.Rating) ?? 0;

            var guide = await _context.Guides.FirstOrDefaultAsync(g => g.Id == booking.GuideId);
            if(guide != null)
            {
                guide.Rating = Math.Round(averageRating, 2);
                await _context.SaveChangesAsync();
            }
            var result = new GuideFeedbackDto
            {
                id = feedback.Id,
                GuideId = feedback.GuideId,
                UserId = feedback.UserId,
                UserName = null,
                GuideBookingId = feedback.GuideBookingId,
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                CreatedAt = feedback.CreatedAt
            };
            return ApiResponse<GuideFeedbackDto>.SuccessResponse(result, "Feedback submitted successfully");
        }
    }
}
