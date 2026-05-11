using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.GuidePackage;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuidePackageService
    {
        Task<ApiResponse<string>> AddPackageAsync(int guideId, AddGuidePackageDto dto);

        Task<ApiResponse<List<GuidePackageDto>>> GetMyPackageAsync(int guideId);

        Task<ApiResponse<GuidePackageDto>> GetPackageByIdAsync(int packageId);

        Task<ApiResponse<string>> UpdatePackageAsync(int guideId, int packageId, UpdateGuidePackageDto dto);

        Task<ApiResponse<string>> DeletePackageAsync(int guideId, int packageId);
    }
}
