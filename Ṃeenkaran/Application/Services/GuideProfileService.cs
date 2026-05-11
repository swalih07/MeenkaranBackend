using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class GuideProfileService : IGuideProfileService
    {
        private readonly AppDbContext _context;
        private readonly CloudinaryService _cloudinaryService;

        public GuideProfileService(AppDbContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ApiResponse<GuideProfileDto>> GetMyProfileAsync(int guideId)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.Id == guideId);
            if (guide == null)
            {
                return new ApiResponse<GuideProfileDto>
                {
                    Success = false,
                    Message = "Guide not found",
                    StatusCode = 404
                };
            }
            return new ApiResponse<GuideProfileDto>
            {
                Success = true,
                Message = "Guide profile loaded successfully",
                StatusCode = 200,
                Data = new GuideProfileDto
                {
                    Id = guideId,
                    Name = guide.Name,
                    Email = guide.Email,
                    PhoneNumber = guide.PhoneNumber,
                    ProfileImageUrl = guide.ProfileImageUrl,
                    IdProofUrl = guide.IdProofUrl,
                    Bio= guide.Bio,
                    Skills= guide.Skills,
                    Area = guide.Area,
                    FishingStyle = guide.FishingStyle,
                    ExperienceYears = guide.ExperienceYears,
                    PricePerDay = guide.PricePerDay,
                    IsApproved = guide.IsApproved,
                    IsAvailabe = guide.IsAvailable,
                    Rating = guide.Rating
                }
            };
        }

        public async Task<ApiResponse<string>> UpdateMyProfileAsync(int guideid, UpdateGuideProfileDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.Id == guideid);

            if (guide == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Guide not found",
                    StatusCode = 404
                };
            }

            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != guide.Email)
            {
                var emailExists = await _context.Guides.AnyAsync(x => x.Email == dto.Email && x.Id != guideid);
                if (emailExists)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Email already exists",
                        StatusCode = 400
                    };
                }
                guide.Email = dto.Email;
            }

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber) && dto.PhoneNumber != guide.PhoneNumber)
            {
                var phoneExists = await _context.Guides.AnyAsync(x => x.PhoneNumber == dto.PhoneNumber && x.Id != guideid);
                if (phoneExists)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Phone number already exists",
                        StatusCode = 400
                    };
                }
                guide.PhoneNumber = dto.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name))
                guide.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Bio))
                guide.Bio = dto.Bio;

            if (!string.IsNullOrWhiteSpace(dto.Skills))
                guide.Skills = dto.Skills;

            if (!string.IsNullOrWhiteSpace(dto.Area))
                guide.Area = dto.Area;

            if (!string.IsNullOrWhiteSpace(dto.FishingStyle))
                guide.FishingStyle = dto.FishingStyle;

            if (dto.ExperienceYears.HasValue)
                guide.ExperienceYears = dto.ExperienceYears.Value;

            if (dto.PricePerDay.HasValue)
                guide.PricePerDay = dto.PricePerDay.Value;

            if (dto.ProfileImage != null)
                guide.ProfileImageUrl = await _cloudinaryService.UploadImageAsync(dto.ProfileImage);

            if (dto.IdProof != null)
                guide.IdProofUrl = await _cloudinaryService.UploadImageAsync(dto.IdProof);

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Guide profile updated successfully",
                StatusCode = 200,
                Data = "Updated"
            };
        }
        public async Task<ApiResponse<GuideFullProfileDto>>GetGuideProfileAsync(int guideId)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.Id == guideId);

            if (guide == null)
            {
                return new ApiResponse<GuideFullProfileDto>
                {
                    Success = false,
                    Message = "Guide not found",
                    StatusCode = 404
                };
            }
            return new ApiResponse<GuideFullProfileDto>
            {
                Success = true,
                Message = "Guide profile fetched successfully",
                StatusCode = 200,
                Data = new GuideFullProfileDto
                {
                    Id = guide.Id,
                    Name = guide.Name,
                    Email = guide.Email,
                    PhoneNumber = guide.PhoneNumber,
                    ProfileImageUrl = guide.ProfileImageUrl,
                    IdProofUrl = guide.IdProofUrl,
                    Bio = guide.Bio,
                    Skills = guide.Skills,
                    Area = guide.Area,
                    FishingStyle = guide.FishingStyle,
                    ExperienceYear = guide.ExperienceYears,
                    PricePerDay = guide.PricePerDay,
                    IsApproved = guide.IsApproved,
                    IsAvailable = guide.IsAvailable,
                    Rating = guide.Rating
                }
            };
        }
    }
}

