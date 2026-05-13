using Ṃeenkaran.Domain.Enums;

namespace Ṃeenkaran.Domain.Entities.Admin
{
    public class TripBooking
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int GuideId { get; set; }

        public decimal GuideAmount { get; set; }
        public decimal CommissionPercent { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal UserServiceCharge { get; set; }
        public decimal TotalUserPays { get; set; }
        public decimal NetGuideEarnings { get; set; }

        public BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;

        public bool IsPayoutReleased { get; set; } = false;
        public DateTime? PayoutReleasedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }
}
