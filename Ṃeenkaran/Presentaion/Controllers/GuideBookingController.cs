using Ṃeenkaran.Application.DTOs.Booking;
using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/user/guide-booking")]
    public class GuideBookingController : ControllerBase
    {
        private readonly IGuideBookingService _bookingService;

        public GuideBookingController(IGuideBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        //[HttpPost]
        ////public async Task<IActionResult>BookGuide(CreateGuideBookingDto dto)
        ////{
        ////    var result = await _bookingService.BookGuideAsync(dto);

        ////    return Ok(result);
        ////}


        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(userIdClaim,out var userId))
            {
                return Unauthorized(new
                {
                    Message = "Invalid token",
                });
            }
            var result = await _bookingService.CreateBookingAsync(userId, dto);

            return StatusCode(result.StatusCode, result);
        }
        [Authorize(Roles = "Guide")]
        [HttpGet("guide")]
        public async Task<IActionResult> GetGuideBooking()
        {
            var guideIdClaim=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(guideIdClaim,out var guideId))
            {
                return Unauthorized(new
                {
                    Message = "Invalid token",
                });
            }
            var result = await _bookingService.GetGuideBookingRequestsAsync(guideId);

            return StatusCode(result.StatusCode, result);
        }
        [Authorize(Roles = "Guide")]
        [HttpPut("{bookingId}/status")]
        public async Task<IActionResult>UpdateBookingStatus(int bookingId, [FromBody]UpdateBookingStatusDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var guideIdClaim=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(guideIdClaim,out var guideId))
            {
                return Unauthorized(new
                {
                    Message = "Invalid token",
                });
            }
            var result = await _bookingService.UpdateBookingStatusAsync(guideId, bookingId, dto);

            return StatusCode(result.StatusCode, result);
        }
    }
}
