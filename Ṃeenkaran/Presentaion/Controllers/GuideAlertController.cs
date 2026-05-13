using Ṃeenkaran.Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [Route("api/guide/alerts")]
    [ApiController]
    [Authorize(Roles = "Guide")]
    public class GuideAlertController : ControllerBase
    {
        private readonly IAdminSystemSafetyService _systemSafetyService;

        public GuideAlertController(IAdminSystemSafetyService systemSafetyService)
        {
            _systemSafetyService = systemSafetyService;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAlerts()
        {
            var result = await _systemSafetyService.GetGuideActiveAlertsAsync();

            return StatusCode(result.StatusCode, result);
        }
    }
}
