using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.FishingSpot;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services.Admin
{
    public class AdminFishingSpotService:IAdminFishingSpotService
    {
        private readonly AppDbContext _context;

        public AdminFishingSpotService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<FishingSpotDto>>> GetPendingSpotRequestsAsync()
        {
            var spots = await (
                from spot in _context.FishingSpots
                join guide in _context.Guides on spot.GuideId equals guide.Id
                where !spot.IsApproved && !spot.IsRemoved
                orderby spot.CreatedAt descending
                select new FishingSpotDto
                {
                    Id = spot.Id,
                    GuideId = spot.GuideId,
                    GuideName = guide.Name,
                    SpotName = spot.SpotName,
                    Description = spot.Description,
                    LocationName = spot.LocationName,
                    Latitude = spot.Latitude,
                    Longitude = spot.Longitude,
                    IsHotspot = spot.IsHotspot,
                    SpotType = spot.SpotType,
                    ImageUrl = spot.ImageUrl ?? string.Empty,
                    IsActive = spot.IsActive,
                    IsApproved = spot.IsApproved,
                    IsRemoved = spot.IsRemoved,
                    ReviewedByAdminId = spot.ReviewedByAdminId,
                    ApprovedAt = spot.ApprovedAt,
                    RemovedAt = spot.RemovedAt,
                    CreatedAt = spot.CreatedAt,
                    UpdatedAt = spot.UpdatedAt
                })
                .ToListAsync();
            return ApiResponse<List<FishingSpotDto>>.SuccessResponse(spots, "Pending spot requests fetched successfully");
        }
        public async Task<ApiResponse<string>>ApproveSpotAsync(int adminId,int spotId)
        {
            var spot = await _context.FishingSpots.FirstOrDefaultAsync(x => x.Id == spotId);

            if (spot == null)
                return ApiResponse<string>.FailResponse("Fishing spot not found", 404);

            if (spot.IsRemoved)
                return ApiResponse<string>.FailResponse("Fishing spot already removed", 400);

            if (spot.IsApproved)
                return ApiResponse<string>.FailResponse("Fishing spot already approved", 400);

            spot.IsApproved = true;
            spot.IsRemoved = false;
            spot.IsActive = true;
            spot.ReviewedByAdminId = adminId;
            spot.ApprovedAt = DateTime.UtcNow;
            spot.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse(null, "Fishing spot approved and published successfully");
        }
        public async Task<ApiResponse<string>> RejectSpotAsync(int adminId, int spotId, RejectFishingSpotDto dto)
        {
            var spot = await _context.FishingSpots.FirstOrDefaultAsync(x => x.Id == spotId);

            if (spot == null)
                return ApiResponse<string>.FailResponse("Fishing spot not found", 404);

            if (spot.IsApproved)
                return ApiResponse<string>.FailResponse("Already approved spot cannot be rejected", 400);

            if (spot.IsRemoved)
                return ApiResponse<string>.FailResponse("Fishing spot already removed", 400);

            spot.IsApproved = false;
            spot.IsRemoved = false;
            spot.IsActive = false;
            spot.RejectReason = dto.Reason;
            spot.ReviewedByAdminId = adminId;
            spot.RejectedAt = DateTime.UtcNow;
            spot.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse(null, "Fishing spot rejected successfully");
        }
        public async Task<ApiResponse<string>>RemoveSpotAsync(int adminId,int spotId)
        {
            var spot = await _context.FishingSpots.FirstOrDefaultAsync(x => x.Id == spotId);

            if (spot == null)
                return ApiResponse<string>.FailResponse("Fishing spot not found", 404);

            spot.IsRemoved = true;
            spot.IsApproved = false;
            spot.IsActive = false;
            spot.ReviewedByAdminId = adminId;
            spot.RemovedAt = DateTime.UtcNow;
            spot.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse(null, "Fishing spot removed successfully");
        }
    }
}
