namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class GuideVerificationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string FishingStyle { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal PricePerDay { get; set; }
        public string ProfileImageUrl { get; set; } = string.Empty;
        public string IdProofUrl { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public string RejectionReason { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public string BlockReason { get; set; } = string.Empty;
    }
}
