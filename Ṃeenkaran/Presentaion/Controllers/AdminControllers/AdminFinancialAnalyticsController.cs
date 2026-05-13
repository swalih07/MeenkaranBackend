using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.Services.Admin;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Application.DTOs.Financial;
using Ṃeenkaran.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers.AdminControllers
{
    [ApiController]
    [Route("api/admin/financial")]
    [Authorize(Roles = "Admin")]
    public class AdminFinancialAnalyticsController : ControllerBase
    {
        private readonly IAdminPlatformAnalyticsService _analyticsService;

        public AdminFinancialAnalyticsController(IAdminPlatformAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("overview")]
        public async Task<IActionResult> GetOverview()
        {
            var result = await _analyticsService.GetOverviewAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("commissions")]
        public async Task<IActionResult> GetCommissions([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _analyticsService.GetCommissionReportAsync(from, to);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("payouts")]
        public async Task<IActionResult> GetPayouts([FromQuery] int? guideId, [FromQuery] string? status)
        {
            var result = await _analyticsService.GetPayoutsAsync(guideId, status);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("payouts/{tripBookingId}/release")]
        public async Task<IActionResult> ReleasePayout(int tripBookingId, [FromBody] ReleaseGuidePayoutRequestDto request)
        {
            var adminIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(adminIdValue, out var adminId))
            {
                return StatusCode(401, ApiResponse<string>.FailResponse("Admin id not found in token.", 401));
            }

            var result = await _analyticsService.ReleasePayoutAsync(
                tripBookingId,
                adminId,
                request.ExternalReference);

            return StatusCode(result.StatusCode, result);
        }
    }
}
