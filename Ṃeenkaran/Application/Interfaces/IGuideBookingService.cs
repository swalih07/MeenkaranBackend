using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Booking;
using Ṃeenkaran.Application.DTOs.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IGuideBookingService
    {
        //Task<List<User> BookGuideAsync(CreateGuideBookingDto dto);

        Task<ApiResponse<string>> CreateBookingAsync(int userId, CreateBookingDto dto);

        Task<ApiResponse<List<BookingRequestDto>>> GetGuideBookingRequestsAsync(int guideId);

        Task<ApiResponse<string>> UpdateBookingStatusAsync(int guideId, int bookingId, UpdateBookingStatusDto dto);
    }
}
