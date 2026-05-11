using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Reports;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuideReportsController:ControllerBase
    {
        private readonly IGuideReportService _reportService;

        public GuideReportsController(IGuideReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("guide/my-analytics")]
        [Authorize(Roles ="Guide")]
        public async Task<IActionResult> GetMyAnalytics()
        {
            var guidIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(!int.TryParse(guidIdClaim,out int guideId))
            {
                return Unauthorized(ApiResponse<string>.FailResponse("Invalid guide token", 401));
            }
            var response = await _reportService.GetMyAnalyticsAsync(guideId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("guide/my-feedbacks")]
        [Authorize(Roles ="Guide")]
        public async Task<IActionResult> GetMyFeedbacks()
        {
            var guideIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(!int.TryParse(guideIdClaim,out int guideId))
            {
                return Unauthorized(ApiResponse<string>.FailResponse("Invalid Guide token", 401));
            }
            var response = await _reportService.GetMyFeedbacksAsync(guideId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("feedback")]
        [Authorize(Roles="User")]
        public  async Task<IActionResult> CreateFeedback([FromBody]CreateGuideFeedbackDto dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!int.TryParse(userIdClaim,out int userId))
            {
                return Unauthorized(ApiResponse<string>.FailResponse("Invalid user token", 401));
            }
            var response = await _reportService.CreateFeedbackAsync(dto, userId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
