using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/user/guide-reviews")]
    public class GuideReviewController:ControllerBase
    {
        private readonly IGuideReviewService _service;

        public GuideReviewController(IGuideReviewService service)
        {
            _service = service;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview([FromBody] CreateGuideReviewDto dto)
        {
            var userIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("Inavalid Token");

            int userId = int.Parse(userIdClaim);

            var result = await _service.AddReviewAsync(userId, dto);

            return Ok(result);
        }

        [HttpDelete("{reviewId}")]
        [Authorize]
        public async Task<IActionResult>DeleteReview(int reviewId)
        {
            var userId = int.Parse(User.FindFirst("id").Value);

            var result = await _service.DeleteReviewAsync(userId, reviewId);

            return Ok(result);
        }
        [HttpGet("guide/{guideId}")]
        public async Task<IActionResult>GetGuideReviews(int guideId)
        {
            var result = await _service.GetGuideReviewsAsync(guideId);
            return Ok(result);
        }
    }
}
