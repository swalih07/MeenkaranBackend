using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class LeaderboardService:ILeaderboardService
    {
        private readonly AppDbContext _context;

        public LeaderboardService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<LeaderboardDto>> GetTopUserAsync()
        {
            return await _context.Users
                .OrderByDescending(x => x.Points)
                .Take(10)
                .Select(x => new LeaderboardDto
                {
                    UserId = x.Id,
                    Name = x.Name,
                    ProfileImage = x.ProfileImageUrl,
                    Points = x.Points,
                    Badge = x.Badge.ToString()
                })
            .ToListAsync();
        }
    }
}
