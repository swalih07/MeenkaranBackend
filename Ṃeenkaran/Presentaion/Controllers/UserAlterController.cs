using Ṃeenkaran.Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{

    [Route("api/user/alerts")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserAlertController : ControllerBase
    {
        private readonly IAdminSystemSafetyService _systemSafetyService;

        public UserAlertController(IAdminSystemSafetyService systemSafetyService)
        {
            _systemSafetyService = systemSafetyService;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAlerts()
        {
            var result = await _systemSafetyService.GetUserActiveAlertsAsync();

            return StatusCode(result.StatusCode, result);
        }
    }
}
