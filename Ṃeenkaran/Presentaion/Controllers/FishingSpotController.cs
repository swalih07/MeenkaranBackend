using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/user/locations")]
    public class FishingSpotController : ControllerBase
    {
        private readonly IFishingSpotService _spotService;

        public FishingSpotController(
            IFishingSpotService spotService)
        {
            _spotService = spotService;
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbySpots(
            double lat,
            double lon,
            double radiusKm = 5)
        {
            var result = await _spotService
                .GetNearbySpotsAsync(
                    lat,
                    lon,
                    radiusKm);

            return Ok(result);
        }
    }
}