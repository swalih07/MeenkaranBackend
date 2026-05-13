namespace Ṃeenkaran.Application.DTOs.Payments
{
    public class CreateGuidePaymentOrderResponseDto
    {
        public int PaymentId { get; set; }

        public string RazorpayKeyId { get; set; } = string.Empty;
        public string RazorpayOrderId { get; set; } = string.Empty;

        public string Currency { get; set; } = "INR";
        public long AmountPaise { get; set; }

        public decimal GuideAmount { get; set; }
        public decimal GuidePlatformFeePercent { get; set; }
        public decimal PlatformFeeFromGuide { get; set; }
        public decimal UserServiceCharge { get; set; }
        public decimal GuideReceives { get; set; }
        public decimal TotalUserPays { get; set; }

        public string? Description{ get; set; }
    }
}
