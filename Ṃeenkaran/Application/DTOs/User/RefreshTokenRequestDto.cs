using System.ComponentModel.DataAnnotations;


namespace Ṃeenkaran.Application.DTOs.User
{
    public class RefreshTokenRequestDto
    {
        [Required(ErrorMessage = "Refresh Token is required")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
