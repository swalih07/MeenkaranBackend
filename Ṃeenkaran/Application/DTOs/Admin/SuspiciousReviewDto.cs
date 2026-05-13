namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class SuspiciousReviewDto
    {
        public int ReviewId { get; set; }

        public int FishingSpotId { get; set; }
        public string FishingSpotName { get; set; } = string.Empty;

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public int Rating { get; set; }
        public string? Comment { get; set; }

        public bool IsSuspicious { get; set; }
        public bool IsHidden { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
