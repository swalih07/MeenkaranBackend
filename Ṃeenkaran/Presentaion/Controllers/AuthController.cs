using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ṃeenkaran.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
        {
            var response = await _authService.RefreshTokenAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var result = await _authService.ForgotPasswordAsync(dto.Email);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = await _authService.GetProfileAsync(email);

            return Ok(result);
        }

        // UPDATE PROFILE
        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileDto dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = await _authService.UpdateProfileAsync(email, dto);

            return Ok(result);
        }
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] UserVerifyOtpDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.VerifyEmailOtpAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}