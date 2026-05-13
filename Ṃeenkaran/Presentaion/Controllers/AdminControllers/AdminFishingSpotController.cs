using Ṃeenkaran.Application.DTOs.FishingSpot;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Application.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers.AdminControllers
{
    [ApiController]
    [Route("api/admin/spots")]
    [Authorize(Roles="Admin")]
    public class AdminFishingSpotController:ControllerBase
    {
        private readonly IAdminFishingSpotService _service;

        public AdminFishingSpotController(IAdminFishingSpotService service)
        {
            _service = service;
        }
        [HttpGet("requests")]
        public async Task<IActionResult> GetPendingRequests()
        {
            var result = await _service.GetPendingSpotRequestsAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("{id}/approve")]
        public async Task<IActionResult>ApproveSpot(int id)
        {
            var adminId = GetCurrentUserId();
            if (adminId <= 0)
            
                return Unauthorized(new
                {
                    Success = false,
                    Message = "Invalid admin id"
                });

            var result = await _service.ApproveSpotAsync(adminId, id);

            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectSpot(int id, [FromBody] RejectFishingSpotDto dto)
        {
            var adminId = GetCurrentUserId();
            if (adminId <= 0)
                return Unauthorized(new { Success = false, Message = "Invalid admin id" });

            var result = await _service.RejectSpotAsync(adminId, id, dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>RemoveSpot(int id)
        {
            var adminId = GetCurrentUserId();
            if (adminId <= 0)
                return Unauthorized(new
                {
                    Success = false,
                    message = "Invalid admin id"
                });
            var result = await _service.RemoveSpotAsync(adminId, id);
            return StatusCode(result.StatusCode, result);
        }
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdClaim, out int userId))
                return userId;

            return 0;
        }
    }
}
