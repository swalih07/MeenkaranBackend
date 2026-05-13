using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user/catch-posts")]
    public class CatchPostController:ControllerBase
    {
        private readonly ICatchPostService _service;

        public CatchPostController(ICatchPostService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm]CreateCatchPostDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0) return Unauthorized("Invalid User");

            var result = await _service.CreatePostAsync(userId, dto);

            return Ok(result);
        }
        [HttpGet("Community-feed")]
        public async Task<IActionResult>GetCommunityFeed(string location)
        {
            var result=await _service.GetCommunityFeedAsync(location);

            return Ok(result);
        }
    }
}
