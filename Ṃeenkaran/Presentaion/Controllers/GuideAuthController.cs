using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/guides/auth")]
    public class GuideAuthController:ControllerBase
    {
        private readonly IGuideAuthService _guideAuthService;

        public GuideAuthController(IGuideAuthService guideAuthService)
        {
            _guideAuthService = guideAuthService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] GuideRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _guideAuthService.RegisterAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] GuideLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _guideAuthService.LoginAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _guideAuthService.RefreshTokenAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgotPassword([FromBody]GuideForgetPasseordDto dto)
        {
            if (!ModelState.IsValid)
            
                return BadRequest(ModelState);

            var result = await _guideAuthService.ForgotPasswordAsync(dto);
            return StatusCode(result.StatusCode, result);
            
        }
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] GuideVerifyOtpDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _guideAuthService.VerifyOtpAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody]GuideResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _guideAuthService.ResetPasswordAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
