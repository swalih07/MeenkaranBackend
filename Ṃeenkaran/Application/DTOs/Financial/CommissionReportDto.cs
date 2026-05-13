namespace Ṃeenkaran.Application.DTOs.Financial
{
    public class CommissionReportDto
    {
        public int TripBookingId { get; set; }
        public int GuideId { get; set; }
        public int UserId { get; set; }

        public decimal GuideAmount { get; set; }
        public decimal CommissionPercent { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal UserServiceCharge { get; set; }
        public decimal TotalUserPays { get; set; }
        public decimal NetGuideEarnings { get; set; }

        public string BookingStatus { get; set; } = string.Empty;
        public bool IsPayoutReleased { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
