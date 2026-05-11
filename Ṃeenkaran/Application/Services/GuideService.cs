using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class GuideService:IGuideService
    {
        private readonly AppDbContext _context;
        private readonly CloudinaryService _cloudinary;
        public GuideService(AppDbContext context,CloudinaryService cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        public async Task<List<GuideDto>>SearchGuidesAsync(string? area,string? style)
        {
            var query = _context.Guides
                .Where(x => x.IsAvailable)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(area))
            {
                query = query.Where(x => x.Area.Contains(area));
            }

            if (!string.IsNullOrWhiteSpace(style))
            {
                query = query.Where(x => x.FishingStyle.Contains(style));
            }

            return await query
                .Select(x => new GuideDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProfileImageUrl = x.ProfileImageUrl,
                    Area = x.Area,
                    FishingStyle = x.FishingStyle,
                    ExperienceYears = x.ExperienceYears,
                    PricePerDay = x.PricePerDay,
                    Rating = x.Rating
                })
                .ToListAsync();
        }
        public async Task<List<GuidePackageDto>> GetGuidePackagesAsync(int guideId)
        {
            return await _context.GuidePackages
                .Where(x => x.GuideId == guideId)
                .Select(x => new GuidePackageDto
                {
                    Id = x.Id,
                    PackageName = x.PackageName,
                    Description = x.Description,
                    Price = x.Price,
                    DurationHours = x.DurationHours,
                    Includes = x.Includes
                })
                .ToListAsync();
        }

        public async Task<List<GuideAvailabilityDto>> GetGuideAvailabilityAsync(int guideId)
        {
            return await _context.GuideAvailabilities
                .Where(x => x.GuideId == guideId)
                .Select(x => new GuideAvailabilityDto
                {
                    GuideId = x.GuideId,
                    GuideName = x.Guide.Name,
                    AvailableDate = x.AvailableDate,
                    TimeSlot = x.TimeSlot,
                    IsBooked = x.IsBooked,
                    Packages = x.Guide.Packages
                    .Select(p => new GuidePackageDto
                    {
                        Id = p.Id,
                        PackageName = p.PackageName,
                        Description = p.Description,
                        Price = p.Price,
                        DurationHours = p.DurationHours,
                        Includes = p.Includes
                    })
                    .ToList()
                })
                .ToListAsync();
        }

    }
}
