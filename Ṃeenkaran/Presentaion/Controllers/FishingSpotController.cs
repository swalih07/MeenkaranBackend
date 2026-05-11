using Ṃeenkaran.Application.DTOs.FishingSpot;
using Ṃeenkaran.Application.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/user/locations")]
    public class FishingSpotController : ControllerBase
    {
        private readonly IFishingSpotService _spotService;

        public FishingSpotController(IFishingSpotService spotService)
        {
            _spotService = spotService;
        }
        [Authorize(Roles ="Guide")]
        [HttpPost]
        public async Task<IActionResult> AddSpot([FromBody]AddFishingSpotDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var guideIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(guideIdClaim,out var guideId))
            {
                return Unauthorized(new
                {
                    Message = "Invalid token"
                });
            }
            var result = await _spotService.AddSpotAsync(guideId, dto);

            return StatusCode(result.StatusCode, result);
        }
        [Authorize(Roles ="Guide")]
        [HttpPut("{spotId}")]
        public async Task<IActionResult>UpdateSpot(int spotId, [FromBody]UpdateFishingSpotDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var guideIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(guideIdClaim,out var guideId))
            {
                return Unauthorized(new
                {
                    Message = "Invalid token"
                });
            }

            var result = await _spotService.UpdateSpotAsync(guideId, spotId, dto);

            return StatusCode(result.StatusCode, result);
        }
        [Authorize(Roles ="Guide")]
        [HttpDelete("{spotId}")]
        public async Task<IActionResult>DeleteSpot(int spotId)
        {
            var guideIdClaim=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int .TryParse(guideIdClaim,out var guideId))
            {
                return Unauthorized(new
                {
                    Message = "Invalid token"
                });
            }
            var result = await _spotService.DeleteSpotAsync(guideId, spotId);

            return StatusCode(result.StatusCode, result);
        }
        [AllowAnonymous]
        [HttpGet("{spotId}")]
        public async Task<IActionResult>GetSpotById(int spotId)
        {
            var result = await _spotService.GetSpotByIdAsync(spotId);

            return StatusCode(result.StatusCode, result);
        }
        [AllowAnonymous]
        [HttpGet("hotspots")]
        public async Task<IActionResult> GetMyAllHotspots()
        {
            var result = await _spotService.GetAllHotspotAsync();

            return StatusCode(result.StatusCode, result);
        }
        [Authorize(Roles ="Guide")]
        [HttpGet("my-spots")]
        public async Task<IActionResult> GetMySpots()
        {
            var guideIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(guideIdClaim,out var guideId))
            {
                return Unauthorized(new
                {
                    Message = "Invalid token"
                });
            }
            var result = await _spotService.GetGuideSpotsAsync(guideId);

            return StatusCode(result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbySpots(
          [FromQuery]  double lat,
          [FromQuery]  double lon,
          [FromQuery]  double radiusKm = 5)
        {
            var result = await _spotService
                .GetNearbySpotsAsync(
                    lat,
                    lon,
                    radiusKm);

            return StatusCode(result.StatusCode, result);
        }
    }
}