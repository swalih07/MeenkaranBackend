using Ṃeenkaran.Application.DTOs.Admin;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers.AdminControllers
{
    [ApiController]
    [Route("api/admin/verification")]
    [Authorize(Roles =Roles.Admin)]
    public class AdminVerificationController:ControllerBase
    {
        private readonly IAdminVerificationService _service;

        public AdminVerificationController(IAdminVerificationService service)
        {
            _service = service;
        }

        [HttpGet("guides/pending")]
        public async Task<IActionResult> GetPendingGuides()
        {
            var response = await _service.GetPendingGuidesAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("guide/{guideId}/verify")]
        public async Task<IActionResult>VerifyGuide(int guideId)
        {
            var response = await _service.VerifyGuideAsync(guideId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("guide/{guideId}/reject")]
        public async Task<IActionResult>RejectGuide(int guideId, [FromBody]RejectedGuideDto dto)
        {
            var response = await _service.RejectGuideAsync(guideId, dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("user/{userId}/block")]
        public async Task<IActionResult>BlockUser(int userId, [FromBody]BlockAccountDto dto)
        {
            var response = await _service.BlockUserAsync( userId, dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("guide/{guideId}/block")]
        public async Task<IActionResult>BlockGuide(int guideId, [FromBody]BlockAccountDto dto)
        {
            var response = await _service.BlockGuideAsync(guideId, dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
