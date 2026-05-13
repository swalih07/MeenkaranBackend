using Ṃeenkaran.Application.DTOs.Admin;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers.AdminControllers
{
    [ApiController]
    [Route("api/admin/safety")]
    [Authorize(Roles ="Admin")]
    public class AdminSystemSafetyController:ControllerBase
    {
        private readonly IAdminSystemSafetyService _service;

        public AdminSystemSafetyController(IAdminSystemSafetyService service)
        {
            _service = service;
        }
        [HttpPost("alerts/broadcast")]
        public async Task<IActionResult> CreateAlert([FromBody]CreateSafetyAlertRequest request)
        {
            var adminId = GetCurrentUserId();

            var adminName = User.Identity?.Name;

            if (adminId < 0)
                return Unauthorized(new { Success = false, Message = "Invalid admin id" });

            var result = await _service.CreateAlertAsync(adminId, adminName, request);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("alerts")]
        public async Task<IActionResult> GetAllAlerts()
        {
            var result = await _service.GetAllAlertsAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("alert/active")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActiveAlerts()
        {
            var result = await _service.GetAllAlertsAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("error")]
        public async Task<IActionResult> GetError()
        {
            var result = await _service.GetSystemErrorsAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("security/suspicious-logins")]
        public async Task<IActionResult> GetSuspiciousLogins()
        {
            var result = await _service.GetSuspiciousLoginAttemptAsync();
            return StatusCode(result.StatusCode, result);
        }
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim, out var userid))
                return userid;

            return 0;
        }
    }
}
