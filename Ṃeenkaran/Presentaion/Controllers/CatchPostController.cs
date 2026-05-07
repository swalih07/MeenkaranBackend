using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
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
            var result = await _service.CreatePostAsync(dto);

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
