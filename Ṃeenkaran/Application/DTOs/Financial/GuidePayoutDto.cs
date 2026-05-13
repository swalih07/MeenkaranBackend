namespace Ṃeenkaran.Application.DTOs.Financial
{
    public class GuidePayoutDto
    {
        public int Id { get; set; }
        public int TripBookingId { get; set; }
        public int GuideId { get; set; }

        public decimal PayoutAmount { get; set; }
        public string Status { get; set; } = string.Empty;

        public int? ReleasedByAdminId { get; set; }
        public string? ExternalReference { get; set; }
        public DateTime? ReleasedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
