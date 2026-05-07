using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.User
{
    public class GuideRegisterDto
    {
        

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2,
           ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Phone number must be exactly 10 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Area is required")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Area must be between 2 and 100 characters")]
        public string Area { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fishing style is required")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Fishing style must be between 2 and 100 characters")]
        public string FishingStyle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Experience years is required")]
        [Range(0, 60,
            ErrorMessage = "Experience years must be between 0 and 60")]
        public int ExperienceYears { get; set; }

        [Required(ErrorMessage = "Price per day is required")]
        [Range(1, 100000,
            ErrorMessage = "Price per day must be between 1 and 100000")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Profile image is required")]
        public IFormFile ProfileImage { get; set; } = default!;

        [Required(ErrorMessage = "ID proof is required")]
        public IFormFile IdProof { get; set; } = default!;
    }
}
