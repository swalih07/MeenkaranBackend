using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/user/post-interactions")]
    public class PostInteractionController:ControllerBase
    {
        private readonly IPostInteractionService _service;

        public PostInteractionController(IPostInteractionService service)
        {
            _service = service;
        }
        [HttpPost("Like")]
        public async Task<IActionResult>ToggleLike(CreateLikeDto dto)
        {
            var result = await _service.ToggleLikeAsync(dto);

            return Ok(result);
        }
        [HttpPost("comment")]
        public async Task<IActionResult>AddComment(CreateCommentDto dto)
        {
            var result = await _service.AddCommentAsync(dto);

            return Ok(result);
        }
        [HttpGet("{postId}/comment")]
        public async Task<IActionResult>GetComments(int postId)
        {
            var result = await _service.GetCommentsAsync(postId);

            return Ok(result);
        }
    }
}
