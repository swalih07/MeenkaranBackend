using Ṃeenkaran.Application.DTOs.Admin;
using Ṃeenkaran.Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers.AdminControllers
{
    [ApiController]
    [Route("api/admin/community")]
    [Authorize(Roles ="Admin")]
    public class AdminCommunityModerationController:ControllerBase
    {
        private readonly IAdminCommunityModerationService _service;

        public AdminCommunityModerationController(IAdminCommunityModerationService service)
        {
            _service = service;
        }

        [HttpGet("posts/reported")]
        public async Task<IActionResult> GetReportedPosts()
        {
            var result = await _service.GetReportedPostAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("posts/{id}")]
        public async Task<IActionResult>RemovePost(int id, [FromBody]RemoveCommunityPostDto dto)
        {
            var adminId = GetCurrentUserId();
            if (adminId <= 0)
                return Unauthorized(new
                {
                    Success = false,
                    Message = "Invalid admin id"
                });
            var result = await _service.RemovePostAsync(adminId, id, dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("reviews/suspicious")]
        public async Task<IActionResult> GetSuspiciousReviews()
        {
            var result = await _service.GetSuspiciousReviewAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut("reviews/{id}/hide")]
        public async Task<IActionResult>HideReview(int id, [FromBody]HideReviewDto dto)
        {
            var adminId = GetCurrentUserId();
            if (adminId <= 0)
                return Unauthorized(new
                {
                    Success = false,
                    Message = "Invalis admin id"
                });
            var result = await _service.HideReviewAsync(adminId, id, dto);
            return StatusCode(result.StatusCode, result);
        }
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdClaim, out var userId))
                return userId;

            return 0;
        }
    }
}
