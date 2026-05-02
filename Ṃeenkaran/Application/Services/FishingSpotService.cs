using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class FishingSpotService : IFishingSpotService
    {
        private readonly AppDbContext _context;

        public FishingSpotService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FishingSpotDto>> GetNearbySpotsAsync(
            double lat,
            double lon,
            double radiusKm = 5)
        {
            var spots = await _context.FishingSpots
                .Where(x => x.IsActive)
                .ToListAsync();

            var nearbySpots = spots
                .Select(x => new FishingSpotDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    SpotType = x.SpotType,
                    ImageUrl = x.ImageUrl,

                    DistanceKm = CalculateDistance(
                        lat,
                        lon,
                        x.Latitude,
                        x.Longitude)
                })
                .Where(x => x.DistanceKm <= radiusKm)
                .OrderBy(x => x.DistanceKm)
                .ToList();

            return nearbySpots;
        }

        private double CalculateDistance(
            double lat1,
            double lon1,
            double lat2,
            double lon2)
        {
            var R = 6371;

            var dLat = DegreesToRadians(lat2 - lat1);

            var dLon = DegreesToRadians(lon2 - lon1);

            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) *
                Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLon / 2) *
                Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(
                Math.Sqrt(a),
                Math.Sqrt(1 - a));

            return R * c;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}