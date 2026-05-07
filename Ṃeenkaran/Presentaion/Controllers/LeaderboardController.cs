using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/user/leaderboard")]
    public class LeaderboardController:ControllerBase
    {
        private readonly ILeaderboardService _service;

        public LeaderboardController(ILeaderboardService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetLeaderboard()
        {
            var result = await _service.GetTopUserAsync();

            return Ok(result);

        }
    }
}
