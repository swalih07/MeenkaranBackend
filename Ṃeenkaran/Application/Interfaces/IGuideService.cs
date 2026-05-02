using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuideService
    {
        Task<List<GuideDto>> SearchGuidesAsync(string? area, string? style);
        Task<List<GuidePackageDto>> GetGuidePackagesAsync(int guideId);

    }
}
