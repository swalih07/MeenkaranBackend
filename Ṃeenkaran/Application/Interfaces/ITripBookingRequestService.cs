using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Domain.Entities.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface ITripBookingRequestService
    {
        Task<string> CreateRequestAsync(CreateTripBookingRequestDto dto);
        Task<IEnumerable<TripBookingRequest>> GetUserBookingAsync(int userId);
        Task<TripBookingRequest?> GetBookingByIdAsync(int bookingId);
        Task<string> CancelbookingAsync(int bookingId);
    }
}
