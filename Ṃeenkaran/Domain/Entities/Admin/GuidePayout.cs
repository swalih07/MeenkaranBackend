using Ṃeenkaran.Domain.Enums;

namespace Ṃeenkaran.Domain.Entities.Admin
{
    public class GuidePayout
    {
        public int Id { get; set; }

        public int TripBookingId { get; set; }
        public int GuideId { get; set; }

        public decimal PayoutAmount { get; set; }

        public PayoutStatus Status { get; set; } = PayoutStatus.Pending;

        public int? ReleasedByAdminId { get; set; }
        public string? ExternalReference { get; set; }
        public DateTime? ReleasedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
