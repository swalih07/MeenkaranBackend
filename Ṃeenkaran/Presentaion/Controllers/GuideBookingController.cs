using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<IActionResult>BookGuide(CreateGuideBookingDto dto)
        {
            var result = await _bookingService.BookGuideAsync(dto);

            return Ok(result);
        }
    }
}
