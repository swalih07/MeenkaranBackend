namespace Ṃeenkaran.Domain.Entities.User
{
    public class GuidePayment
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int GuideId { get; set; }

        public decimal GuideAmount { get; set; }
        public decimal PlatformFeePercent { get; set; }
        public decimal PlatformFeeFromGuide { get; set; }
        public decimal UserServiceCharge { get; set; }
        public decimal TotalUserPays { get; set; }
        public decimal GuideReceives { get; set; }


        public string Currency { get; set; } = "INR";

        public string? RazorpayOrderId { get; set; }
        public string? RazorpayPaymentId { get; set; }
        public string? RazorpaySignature { get; set; }

        public string PaymentStatus { get; set; } = "Pending";

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }
    }
}
