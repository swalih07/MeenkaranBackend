using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.FishingSpot;
using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Infrastructure.Data;
using Ṃeenkaran.Migrations;
using Ṃeenkaran.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;
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

        //AddSpot
        public  async Task<ApiResponse<string>>AddSpotAsync(int guideId,AddFishingSpotDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.Id == guideId);

            if(guide == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Guide not found",
                    StatusCode = 404
                };
            }
            var spot = new FishingSpot
            {
                GuideId = guideId,
                SpotName = dto.SpotName,
                Description = dto.Description,
                LocationName = dto.LocationName,
                Latitude = (double)dto.Latitude,
                Longitude = (double)dto.Longitude,
                SpotType = dto.SpotType,
                ImageUrl = dto.ImageUrl ?? string.Empty,
                IsHotspot = dto.IsHotspot,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };
            await _context.FishingSpots.AddAsync(spot);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Fishing spot added successfully",
                StatusCode = 200,
                Data = "Created"
            };
        }
        //UpdateSpot
        public async Task<ApiResponse<string>>UpdateSpotAsync(int guideId,int spotId,UpdateFishingSpotDto dto)
        {
            var spot = await _context.FishingSpots
                .FirstOrDefaultAsync(x => x.Id == spotId && x.GuideId == guideId && x.IsActive);

            if (spot == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Fishing spot not found",
                    StatusCode = 404
                };
            }
            if (!string.IsNullOrWhiteSpace(dto.SpotName))
                spot.SpotName = dto.SpotName;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                spot.Description = dto.Description;

            if (!string.IsNullOrWhiteSpace(dto.LocationName))
                spot.LocationName = dto.LocationName;

            if (dto.Latitude != 0)
                spot.Latitude = (double)dto.Latitude;

            if (dto.Longitude != 0)
                spot.Longitude = (double)dto.Longitude;

            if (!string.IsNullOrWhiteSpace(dto.SpotType))
                spot.SpotType = dto.SpotType;

            if (!string.IsNullOrWhiteSpace(dto.ImageUrl))
                spot.ImageUrl = dto.ImageUrl;

            if (dto.IsHotspot)
                spot.IsHotspot = dto.IsHotspot;

            if (dto.IsActive)
                spot.IsActive = dto.IsActive;

            spot.UpdatedAt= DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Fishing spot updated successfully",
                StatusCode = 200,
                Data = "Updated"
            };
        }
        //DeleteSpot
        public async Task<ApiResponse<string>>DeleteSpotAsync(int guideId,int spotId)
        {
            var spot = await _context.FishingSpots
                .FirstOrDefaultAsync(x => x.Id == spotId && x.GuideId == guideId && x.IsActive);

            if(spot == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Fishing spot not found",
                    StatusCode = 404
                };
            }

            spot.IsActive = false;
            spot.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Fishing spot deleted successfully",
                StatusCode = 200,
                Data = "Deleted"
            };
        }
        //GetSpotById
        public async Task<ApiResponse<FishingSpotDto>>GetSpotByIdAsync(int spotId)
        {
            var spot = await _context.FishingSpots
                .Include(x => x.Guide)
                .FirstOrDefaultAsync(x => x.Id == spotId && x.IsActive);
            if(spot == null)
            {
                return new ApiResponse<FishingSpotDto>
                {
                    Success = false,
                    Message = "Fishing spot not found",
                    StatusCode = 404
                };
            }
            return new ApiResponse<FishingSpotDto>
            {
                Success = true,
                Message = "Fishing spot fetched successfully",
                StatusCode = 200,
                Data = new FishingSpotDto
                {
                    Id = spot.Id,
                    SpotName = spot.SpotName,
                    Description = spot.Description,
                    Latitude = (decimal)spot.Latitude,
                    Longitude = (decimal)spot.Longitude,
                    SpotType = spot.SpotType,
                    ImageUrl = spot.ImageUrl,
                }
            };
        }
        //GetAllHotspots
        public async Task<ApiResponse<List<FishingSpotDto>>> GetAllHotspotAsync()
        {
            var spot = await _context.FishingSpots
                .Where(x => x.IsActive && x.IsHotspot)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new FishingSpotDto
                {
                    Id = x.Id,
                    SpotName = x.SpotName,
                    Description = x.Description,
                    Latitude = (decimal)x.Latitude,
                    Longitude = (decimal)x.Longitude,
                    SpotType = x.SpotType,
                    ImageUrl = x.ImageUrl

                })
                .ToListAsync();

            return new ApiResponse<List<FishingSpotDto>>
            {
                Success = true,
                Message = "Hotspots fetched successfully",
                StatusCode = 200,
                Data = spot
            };
        }
        //GetGuideSpots
        public async Task<ApiResponse<List<FishingSpotDto>>>GetGuideSpotsAsync(int guideId)
        {
            var spots=await _context.FishingSpots
                .Where(x=>x.GuideId==guideId && x.IsActive)
                .OrderByDescending(x=>x.CreatedAt)
                .Select(x=> new FishingSpotDto
                {
                    Id=x.Id,
                    SpotName=x.SpotName,
                    Description = x.Description,
                    Latitude = (decimal)x.Latitude,
                    Longitude = (decimal)x.Longitude,
                    SpotType=x.SpotType,
                    ImageUrl=x.ImageUrl
                })
                .ToListAsync();

            return new ApiResponse<List<FishingSpotDto>>
            {
                Success = true,
                Message = "Guide spot fetched successfully",
                StatusCode = 200,
                Data = spots
            };
        }
        //NearbySpots
        public async Task<ApiResponse<List<NearbyFishingSpotDto>>> GetNearbySpotsAsync(
            double lat,
            double lon,
            double radiusKm = 5)
        {
            var spots = await _context.FishingSpots
                .Where(x => x.IsActive)
                .ToListAsync();

            var nearbySpots = spots
                .Select(x => new NearbyFishingSpotDto
                {
                    Id = x.Id,
                    Name = x.SpotName,
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

            return new ApiResponse<List<NearbyFishingSpotDto>>
            {
                Success = true,
                Message = "Nearby fishing spots fetched successfully",
                StatusCode = 200,
                Data = nearbySpots
            };

        }

        private double CalculateDistance(
            double lat1,
            double lon1,
            double lat2,
            double lon2)
        {
            const double R = 6371;

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