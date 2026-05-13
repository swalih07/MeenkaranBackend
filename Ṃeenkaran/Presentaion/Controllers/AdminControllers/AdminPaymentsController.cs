using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Application.DTOs.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers.AdminControllers
{
    [Route("api/admin/payments")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminPaymentsController : ControllerBase
    {
        private readonly IAdminPaymentService _service;

        public AdminPaymentsController(IAdminPaymentService service)
        {
            _service = service;
        }

        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            var result = await _service.GetSettingsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] UpdatePlatformPaymentSettingRequestDto request)
        {
            var adminId = GetCurrentUserId();
            if (adminId <= 0)
                return Unauthorized(new { Success = false, Message = "Invalid admin id" });

            var result = await _service.UpdateSettingsAsync(adminId, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPayments()
        {
            var result = await _service.GetAllPaymentsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetPaymentByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReport()
        {
            var result = await _service.GetReportAsync();
            return StatusCode(result.StatusCode, result);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
    }
}
