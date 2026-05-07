using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class ActiveFishingFeedService:IActiveFishingFeedService
    {
        private readonly AppDbContext _context;

        public ActiveFishingFeedService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ActiveFishingFeedDto>> GetLiveFeedsAsync()
        {
            return await _context.ActiveFishingFeeds
                .Include(x=>x.Guide)
                .OrderByDescending(x=>x.UpdatedAt)
                .Select(x=>new ActiveFishingFeedDto
                {
                    Id = x.Id,
                    SpotName = x.SpotName,
                    Area = x.Area,
                    FishType = x.FishType,
                    ActiveBoats = x.ActiveBoats,
                    Weather=x.Weather,
                    IsHotSpot = x.IsHotSpot,
                    UpdatedAt = x.UpdatedAt,
                    GuideName = x.Guide.Name
                })
                .ToListAsync();
        }
    }
}
