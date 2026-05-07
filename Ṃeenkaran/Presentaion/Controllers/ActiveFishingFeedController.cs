using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/user/live-feeds")]
    public class ActiveFishingFeedController : ControllerBase
    {
        private readonly IActiveFishingFeedService _feedService;

        public ActiveFishingFeedController(IActiveFishingFeedService feedService)
        {
            _feedService = feedService;
        }
        [HttpGet]
        public async Task<IActionResult> GetLiveFeeds()
        {
            var feeds = await _feedService.GetLiveFeedsAsync();
            return Ok(feeds);
        }
    }
}
