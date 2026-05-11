namespace Ṃeenkaran.Application.DTOs.User
{
    public class GuideProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = string.Empty;
        public string IdProofUrl { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string FishingStyle { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsApproved { get; set; }
        public bool IsAvailabe { get; set; }
        public double Rating { get; set; }

    }
}
