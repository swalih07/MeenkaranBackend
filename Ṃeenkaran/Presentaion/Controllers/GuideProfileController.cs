using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/guides/profile")]
    [Authorize(Roles ="Guide")]
    public class GuideProfileController:ControllerBase
    {
        private readonly IGuideProfileService _guideProfileService;

        public GuideProfileController(IGuideProfileService guideProfileService)
        {
            _guideProfileService = guideProfileService;
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var guideIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(guideIdClaim, out var guideId))
                return Unauthorized(new { Message = "Invalid token" });

            var result = await _guideProfileService.GetMyProfileAsync(guideId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut("me")]
        [Consumes("multipart/from-data")]
        public async Task<IActionResult> UpdateMyProfile([FromForm] UpdateGuideProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var guideIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(guideIdClaim, out var guideId))
                return Unauthorized(new { Message = "Invalid Token" });

            var result = await _guideProfileService.UpdateMyProfileAsync(guideId, dto);
            return StatusCode(result.StatusCode, result);
        }
        [AllowAnonymous]
        [HttpGet("{guideId}")]
        public async Task<IActionResult>GetGuideById(int guideId)
        {
            var result = await _guideProfileService.GetGuideProfileAsync(guideId);
            return StatusCode(result.StatusCode, result);
        }
        
    }
}
