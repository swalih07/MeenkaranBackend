using Ṃeenkaran.Domain.Entities.User;

namespace Ṃeenkaran.Domain.Entities.Admin
{
    public class FishingSpotReview
    {
        public int Id { get; set; }

        public int FishingSpotId { get; set; }
        public FishingSpot FishingSpot { get; set; } = null!;

        public int UserId { get; set; }
        public Ṃeenkaran.Domain.Entities.User.User User { get; set; } = null!;

        public int Rating { get; set; }
        public string? Comment { get; set; }

        public bool IsSuspicious { get; set; } = false;
        public bool IsHidden { get; set; } = false;

        public string? HiddenReason { get; set; }
        public int? ReviewedByAdminId { get; set; }
        public DateTime? HiddenAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
