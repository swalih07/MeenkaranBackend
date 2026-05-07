using Ṃeenkaran.Domain.Enums;

namespace Ṃeenkaran.Domain.Entities.User
{
    public class TripBookingRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int GuideId { get; set; }

        public Guide Guide { get; set; }

        public DateTime TripDate { get; set; }

        public string TimeSlot { get; set; } = string.Empty;

        public string GearRequirements { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    }
}
