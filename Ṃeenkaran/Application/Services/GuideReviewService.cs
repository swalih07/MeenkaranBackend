using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Domain.Enums;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class GuideReviewService : IGuideReviewService
    {
        private readonly AppDbContext _context;

        public GuideReviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string>AddReviewAsync(int userId,CreateGuideReviewDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                return "Rating must be between 1 and 5";

            var booking = await _context.TripBookingRequests
                .FirstOrDefaultAsync(x =>
                x.Id == dto.BookingId &&
                x.UserId == userId);

            if (booking == null)
                return "Invalid booking";

            if (booking.Status != BookingStatus.Completed)
                return "You can review only after trip completion";

            var alreadyReviewed = await _context.GuideReviews
                .AnyAsync(x => x.BookingId == dto.BookingId);

            if (alreadyReviewed)
                return "You already reviewed this booking";

            var review = new GuideReview
            {
                UserId = userId,
                GuideId = booking.GuideId,
                BookingId = dto.BookingId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            await _context.GuideReviews.AddAsync(review);

            await _context.SaveChangesAsync();

            var avgRating = await _context.GuideReviews
                .Where(x => x.GuideId == booking.GuideId)
                .AverageAsync(x => x.Rating);

            var guide = await _context.Guides
                .FindAsync(booking.GuideId);

            if(guide != null)
            {
                guide.Rating = Math.Round(avgRating, 1);
                await _context.SaveChangesAsync();
            }

            return "Review added successfully";
        }
        public async Task<string>DeleteReviewAsync(int userId,int reviewId)
        {
            var review = await _context.GuideReviews
                .FirstOrDefaultAsync(x => x.Id == reviewId);

            if(review==null)
                return "Review not found";

            if (review.UserId != userId)
                return "You are not allowed to delete this review";

            _context.GuideReviews.Remove(review);

            await _context.SaveChangesAsync();

            var rating = await _context.GuideReviews
                .Where(x => x.GuideId == review.GuideId)
                .Select(x => x.Rating)
                .ToListAsync();

            var guide = await _context.Guides
                .FindAsync(review.GuideId);

            if(guide != null)
            {
                guide.Rating = rating.Count == 0 ? 0 : Math.Round(rating.Average(), 1);

                await _context.SaveChangesAsync();
            }
            return "Review deleted successfully";
        }
        public async Task<List<GuideReviewDto>>GetGuideReviewsAsync(int guideId)
        {
            return await _context.GuideReviews
                .Include(x=>x.User)
                .Where(x=>x.GuideId == guideId)
                .OrderByDescending(x=>x.CreatedAt)
                .Select(x=>new GuideReviewDto
                {
                    ReviewId=x.Id,
                    UserName=x.User.Name,
                    UserProfileImage=x.User.ProfileImageUrl,
                    Rating=x.Rating,
                    Comment=x.Comment,
                    CreatedAt=x.CreatedAt
                })
                .ToListAsync();
        }
    }
}
