using CloudinaryDotNet;
using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Admin;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services.Admin
{
    public class AdminCommunityModerationService:IAdminCommunityModerationService
    {
        private readonly AppDbContext _context;

        public AdminCommunityModerationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<List<ReportedCommunityPostDto>>> GetReportedPostAsync()
        {
            var posts = await (
                from post in _context.CommunityPosts
                join user in _context.Users on post.UserId equals user.Id
                join report in _context.CommunityPostReports on post.Id equals report.CommunityPostId
                group new { post, user, report } by new
                {
                    post.Id,
                    post.UserId,
                    user.Name,
                    post.Content,
                    post.ImageUrl,
                    post.IsActive,
                    post.IsRemoved,
                    post.CreatedAt
                }
                into g
                orderby g.Max(x => x.report.CreatedAt) descending
                select new ReportedCommunityPostDto
                {
                    PostId = g.Key.Id,
                    PostUserId = g.Key.UserId,
                    PostUserName = g.Key.Name,
                    Content = g.Key.Content,
                    ImageUrl = g.Key.ImageUrl,
                    ReportCount = g.Count(),
                    LatestReason = g.OrderByDescending(x => x.report.CreatedAt).Select(x => x.report.Reason).FirstOrDefault(),
                    IsActive = g.Key.IsActive,
                    IsRemoved = g.Key.IsRemoved,
                    CreatedAt = g.Key.CreatedAt
                }
                ).ToListAsync();
            return ApiResponse<List<ReportedCommunityPostDto>>.SuccessResponse(posts, "Reported posts fetched successfully");
        }
        public async Task<ApiResponse<string>>RemovePostAsync(int adminId,int postId,RemoveCommunityPostDto dto)
        {
            var post = await _context.CommunityPosts.FirstOrDefaultAsync(x => x.Id == postId);

            if (post == null)
                return ApiResponse<string>.FailResponse("Community post not found", 404);

            if (post.IsRemoved)
                return ApiResponse<string>.FailResponse("Post already removed", 400);



            post.IsRemoved = true;
            post.IsActive = false;
            post.RemovalReason = dto.Reason;
            post.ReviewedByAdmin = adminId;
            post.RemovedAt = DateTime.UtcNow;
            post.UpdatedAt = DateTime.UtcNow;

            var reports = await _context.CommunityPostReports
                .Where(x => x.CommunityPostId == postId && !x.IsResolved)
                .ToListAsync();

            foreach (var report in reports)
            {
                report.IsResolved = true;
                report.ResolvedByAdminId = adminId;
                report.ResolvedAt = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();


            return ApiResponse<string>.SuccessResponse(null, "Community post removed sucessfully");
        }
        public async Task<ApiResponse<List<SuspiciousReviewDto>>> GetSuspiciousReviewAsync()
        {
            var reviews = await (
                from review in _context.FishingSpotReviews
                join spot in _context.FishingSpots on review.FishingSpotId equals spot.Id
                join user in _context.Users on review.UserId equals user.Id
                where review.Rating <= 2 || review.IsSuspicious || review.IsHidden
                orderby review.CreatedAt descending
                select new SuspiciousReviewDto
                {
                    ReviewId = review.Id,
                    FishingSpotId = spot.Id,
                    FishingSpotName = spot.SpotName,
                    UserId = user.Id,
                    UserName = user.Name,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    IsSuspicious = review.IsSuspicious,
                    IsHidden = review.IsHidden,
                    CreatedAt = review.CreatedAt
                })
                .ToListAsync();
            return ApiResponse<List<SuspiciousReviewDto>>.SuccessResponse(reviews, "Suspicious reviews fetched successfully");
        }
        public async Task<ApiResponse<string>>HideReviewAsync(int adminId,int reviewId,HideReviewDto dto)
        {
            var review = await _context.FishingSpotReviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (review == null)
                return ApiResponse<string>.FailResponse("Review not found", 404);

            if (review.IsHidden)
            
                return ApiResponse<string>.FailResponse("Review already hidden", 400);

            review.IsHidden = true;
            review.IsSuspicious = true;
            review.HiddenReason = dto.Reason;
            review.ReviewedByAdminId = adminId;
            review.HiddenAt = DateTime.UtcNow;
            review.UpdatedAt = DateTime.UtcNow;


            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse(null, "Review hidden successfully");

            
        }
    }
}
