using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.User
{
    public class UpdateGuideProfileDto
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public string? PhoneNumber { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Area must be between 2 and 100 characters")]
        public string? Area { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Fishing style must be between 2 and 100 characters")]
        public string? FishingStyle { get; set; }

        [Range(0, 60, ErrorMessage = "Experience years must be between 0 and 60")]
        public int? ExperienceYears { get; set; }

        [Range(1, 100000, ErrorMessage = "Price per day must be between 1 and 100000")]
        public decimal? PricePerDay { get; set; }

        public IFormFile? ProfileImage { get; set; }
        public IFormFile? IdProof { get; set; }
    }
}
