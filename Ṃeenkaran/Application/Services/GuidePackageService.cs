using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.GuidePackage;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ṃeenkaran.Domain.Entities.User;

namespace Ṃeenkaran.Application.Services
{
    public class GuidePackageService : IGuidePackageService
    {
        private readonly AppDbContext _context;


        public GuidePackageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<string>>AddPackageAsync(int guideId,AddGuidePackageDto dto)
        {
            var guide = await _context.Guides.FirstOrDefaultAsync(x => x.Id == guideId);

            if (guide == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Guide not found",
                    StatusCode = 404
                };
            }

            var package = new GuidePackage
            {
                GuideId = guideId,
                PackageName = dto.PackageName,
                Description = dto.Description,
                Price = dto.Price,
                DurationHours = dto.DurationHours,
                Includes = dto.Includes,
                TripLocation = dto.TripLocation,
                IsActive = true
            };

            await _context.GuidePackages.AddAsync(package);

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Package added successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<List<GuidePackageDto>>>GetMyPackageAsync(int guideId)
        {
            var packages = await _context.GuidePackages
                .Where(x => x.GuideId == guideId && x.IsActive)
                .Select(x => new GuidePackageDto
                {
                    Id = x.Id,
                    PackageName = x.PackageName,
                    Description = x.Description,
                    price = x.Price,
                    DurationHours = x.DurationHours,
                    Includes = x.Includes,
                    TripLocation = x.TripLocation,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return new ApiResponse<List<GuidePackageDto>>
            {
                Success = true,
                Message = "Package fetched successfully",
                StatusCode = 200,
                Data = packages
            };
        }

        public async Task<ApiResponse<GuidePackageDto>>GetPackageByIdAsync(int packageId)
        {
            var package = await _context.GuidePackages
                .Where(x => x.Id == packageId)
                .Select(x => new GuidePackageDto
                {
                    Id = x.Id,
                    PackageName = x.PackageName,
                    Description = x.Description,
                    price = x.Price,
                    DurationHours = x.DurationHours,
                    Includes = x.Includes,
                    TripLocation = x.TripLocation,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();

            if(package == null)
            {
                return new ApiResponse<GuidePackageDto>
                {
                    Success = false,
                    Message = "Package not found",
                    StatusCode = 404
                };
            }
            return new ApiResponse<GuidePackageDto>
            {
                Success = true,
                Message = "Package fetched successfully",
                StatusCode = 200,
                Data = package
            };
        }
        public async Task<ApiResponse<string>>UpdatePackageAsync(int guideId,int packageId,UpdateGuidePackageDto dto)
        {
            var package = await _context.GuidePackages.FirstOrDefaultAsync(x =>
                    x.Id == packageId &&
                    x.GuideId == guideId);

            if(package == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Package not found",
                    StatusCode = 404
                };
            }
            package.PackageName = dto.PackageName;
            package.Description = dto.Description;
            package.Price = dto.Price;
            package.DurationHours = dto.DurationHours;
            package.Includes = dto.Includes;
            package.TripLocation = dto.TripLocation;

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Package Updated Successfullt",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<string>>DeletePackageAsync(int guideId,int packageId)
        {
            var package = await _context.GuidePackages.FirstOrDefaultAsync(x =>
                    x.Id == packageId &&
                    x.GuideId == guideId);

            if(package == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Package not found",
                    StatusCode = 404
                };
            }

            package.IsActive = false;

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Package Deleted Successfully",
                StatusCode = 200
            };
        }
    }
}
