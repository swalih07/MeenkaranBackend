using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/user/guides")]
    public class GuideController : ControllerBase
    {
        private readonly IGuideService _guideService;

        public GuideController(IGuideService guideService)
        {
            _guideService = guideService;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchGuides(string? area, string? style)
        {
            var result = await _guideService
                .SearchGuidesAsync(area, style);

            return Ok(result);
        }
        [HttpGet("{guideId}/packages")]
        public async Task<IActionResult> GetPackages(int guideId)
        {
            var result = await _guideService.GetGuidePackagesAsync(guideId);

            return Ok(result);
        }
        [HttpGet("{guideId}/availability")]
        public async Task<IActionResult>GetGuideAvailability(int guideId)
        {
            var result = await _guideService
                .GetGuideAvailabilityAsync(guideId);

            return Ok(result);
        }
    }
}
