using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Admin;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services.Admin
{
    public class AdminVerificationService:IAdminVerificationService
    {
        private readonly AppDbContext _context;

        public AdminVerificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<GuideVerificationDto>>> GetPendingGuidesAsync()
        {
            var guides = await _context.Guides
                .Where(g => !g.IsApproved && !g.IsRejected && !g.IsBlocked)
                .Select(g => new GuideVerificationDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Email = g.Email,
                    PhoneNumber = g.PhoneNumber,
                    Area = g.Area,
                    FishingStyle = g.FishingStyle,
                    ExperienceYears = g.ExperienceYears,
                    PricePerDay = g.PricePerDay,
                    ProfileImageUrl = g.ProfileImageUrl,
                    IdProofUrl = g.IdProofUrl,
                    IsApproved = g.IsApproved,
                    IsRejected = g.IsRejected,
                    RejectionReason = g.RejectionReason,
                    IsBlocked = g.IsBlocked,
                    BlockReason = g.BlockReason
                })
                .ToListAsync();

            return ApiResponse<List<GuideVerificationDto>>.SuccessResponse(guides, "Pending guides loaded successfully", 200);
        }
        public async Task<ApiResponse<string>>VerifyGuideAsync(int guideId)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(g => g.Id == guideId);

            if(guide == null)
            {
                return ApiResponse<string>.FailResponse("Guide not found", 404);
            }

            if (guide.IsBlocked)
            {
                return ApiResponse<string>.FailResponse("Guide is blocked", 403);
            }
            if (string.IsNullOrWhiteSpace(guide.IdProofUrl))
            {
                return ApiResponse<string>.FailResponse("Guide ID proof is missing", 400);
            }

            guide.IsApproved = true;
            guide.IsRejected = false;
            guide.RejectionReason = null;
            guide.IsBlocked = false;
            guide.BlockReason = null;

            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Guide verified and marked as live", "Guide verified successfully", 200);
        }
        public async Task<ApiResponse<string>>RejectGuideAsync(int guideId,RejectedGuideDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(g => g.Id == guideId);

            if(guide == null)
            
                return ApiResponse<string>.FailResponse("Guide not found", 404);
            

            if (guide.IsApproved)
            
                return ApiResponse<string>.FailResponse("Already approved guide cannot be rejected", 404);

            guide.IsApproved = false;
            guide.IsRejected = true;
            guide.RejectionReason = dto.Reason;
            guide.IsBlocked = false;
            guide.BlockReason = null;

            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Guide application rejected", "Guide rejected successfully", 200);

        }
        public async Task<ApiResponse<string>>BlockUserAsync(int userId,BlockAccountDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return ApiResponse<string>.FailResponse("User not found", 404);

            user.IsBlocked = true;
            user.BlockReason = dto.Reason;

            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("User blocked successfully", "User blocked successfully", 200);
        }
        public async Task<ApiResponse<string>>BlockGuideAsync(int guideId,BlockAccountDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(g => g.Id == guideId);

            if (guide == null)
                return ApiResponse<string>.FailResponse("Guide not found", 404);

            guide.IsBlocked = true;
            guide.BlockReason = dto.Reason;

            guide.IsApproved = false;

            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Guide blocked successfully", "Guide blocked successfully", 200);
        }
    }
}
