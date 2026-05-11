using Ṃeenkaran.Application.DTOs.GuidePackage;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/guides/packages")]
    [Authorize(Roles ="Guide")]
    public class GuidePackageController : ControllerBase
    {
        private readonly IGuidePackageService _guidePackageService;

        public GuidePackageController(IGuidePackageService guidePackageService)
        {
            _guidePackageService = guidePackageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPackage([FromBody]AddGuidePackageDto dto)
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
            var result = await _guidePackageService.AddPackageAsync(guideId, dto);

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetMyPackage()
        {
            var guideIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(guideIdClaim,out var guideId))
            {
                return Unauthorized(new
                {
                    Message = "Invalid token"
                });
            }

            var result = await _guidePackageService.GetMyPackageAsync(guideId);
            return StatusCode(result.StatusCode, result);
        }
        [AllowAnonymous]
        [HttpGet("{packageId}")]
        public async Task<IActionResult>GetPackageById(int packageId)
        {
            var result = await _guidePackageService.GetPackageByIdAsync(packageId);

            return StatusCode(result.StatusCode, result);
        }
        [HttpPut("{packageId}")]
        public async Task<IActionResult>UpdatePackage(int packageId, [FromBody]UpdateGuidePackageDto dto)
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
            var result = await _guidePackageService.UpdatePackageAsync(guideId, packageId, dto);

            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("{packageId}")]
        public async Task<IActionResult>DeletePackage(int packageId)
        {
            var guideIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if(!int.TryParse(guideIdClaim,out var guideId))
            {
                return Unauthorized(new
                {
                    Message = "Invalid token"
                });
            }
            var result = await _guidePackageService.DeletePackageAsync(guideId, packageId);

            return StatusCode(result.StatusCode, result);
        }
    }
}
