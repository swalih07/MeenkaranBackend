using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Application.DTOs.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [Route("api/user/payments")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserPaymentsController : ControllerBase
    {
        private readonly IGuidePaymentService _service;

        public UserPaymentsController(IGuidePaymentService service)
        {
            _service = service;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateGuidePaymentOrderRequestDto request)
        {
            var userId = GetCurrentUserId();
            if (userId <= 0)
                return Unauthorized(new { Success = false, Message = "Invalid user id" });

            var result = await _service.CreateOrderAsync(userId, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyGuidePaymentRequestDto request)
        {
            var userId = GetCurrentUserId();
            if (userId <= 0)
                return Unauthorized(new { Success = false, Message = "Invalid user id" });

            var result = await _service.VerifyPaymentAsync(userId, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> History()
        {
            var userId = GetCurrentUserId();
            if (userId <= 0)
                return Unauthorized(new { Success = false, Message = "Invalid user id" });

            var result = await _service.GetUserPaymentsAsync(userId);
            return StatusCode(result.StatusCode, result);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
    }
}
