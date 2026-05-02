using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class GuideService:IGuideService
    {
        private readonly AppDbContext _context;
        public GuideService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<GuideDto>>SearchGuidesAsync(string? area,string? style)
        {
            var query = _context.Guides
                .Where(x => x.IsAvailable)
                .AsQueryable();

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
                    PackageName = x.PackageName,
                    Description = x.Description,
                    Price = x.Price,
                    Duration = x.Duration,
                    Includes = x.Includes
                })
                .ToListAsync();
        }
    }
}
