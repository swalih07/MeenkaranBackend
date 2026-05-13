using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [Route("api/guide/payments")]
    [ApiController]
    [Authorize(Roles = "Guide")]
    public class GuidePaymentsController : ControllerBase
    {
        private readonly IGuideEarningService _service;

        public GuidePaymentsController(IGuideEarningService service)
        {
            _service = service;
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var guideId = GetCurrentUserId();
            if (guideId <= 0)
                return Unauthorized(new { Success = false, Message = "Invalid guide id" });

            var result = await _service.GetMyPaymentsAsync(guideId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var guideId = GetCurrentUserId();
            if (guideId <= 0)
                return Unauthorized(new { Success = false, Message = "Invalid guide id" });

            var result = await _service.GetMySummaryAsync(guideId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetById(int paymentId)
        {
            var guideId = GetCurrentUserId();
            if (guideId <= 0)
                return Unauthorized(new { Success = false, Message = "Invalid guide id" });

            var result = await _service.GetPaymentByIdAsync(guideId, paymentId);
            return StatusCode(result.StatusCode, result);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
    }
}
