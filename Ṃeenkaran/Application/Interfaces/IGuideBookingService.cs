using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuideBookingService
    {
        Task<string> BookGuideAsync(CreateGuideBookingDto dto);
    }
}
