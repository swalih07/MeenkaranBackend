using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/user/trip-bookings")]
    public class TripBookingRequestController:ControllerBase
    {
        private readonly ITripBookingRequestService _service;

        public TripBookingRequestController(ITripBookingRequestService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookingRequest(CreateTripBookingRequestDto dto)
        {
            var result = await _service.CreateRequestAsync(dto);
            return Ok(result);
        }

        [HttpGet("my-bookings")]
        public async Task<IActionResult> GetMyBookings(int userId)
        {
            var bookings = await _service.GetUserBookingAsync(userId);
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingByIdAsync(int id)
        {
            var booking = await _service.GetBookingByIdAsync(id);

            if (booking == null)
                return NotFound("Booking not found");

            return Ok(booking);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var result = await _service.CancelbookingAsync(id);
            if (result == "Booking not found")
                return NotFound(result);

            return Ok(result);
        }
    }
}
