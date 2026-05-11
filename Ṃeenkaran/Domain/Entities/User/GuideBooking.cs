
using Ṃeenkaran.Domain.Enums;
namespace Ṃeenkaran.Domain.Entities.User
{
    public class GuideBooking
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int GuideId { get; set; }

        public Guide Guide { get; set; }

        public int GuidePackageId { get; set; }

        public GuidePackage GuidePackage { get; set; }

        public DateTime BookingDate { get; set; }
        public int PersonCount { get; set; }
        public string Notes { get; set; }=string.Empty;

        //public string Status { get; set; } = "Pending";
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public decimal TotelAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
