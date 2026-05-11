using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.User
{
    public class UserVerifyOtpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }= string.Empty;

        [Required]
        [StringLength(6,MinimumLength =6)]
        public string Otp { get; set; }=string.Empty;
    }
}
