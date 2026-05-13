namespace Ṃeenkaran.Application.DTOs.Payments
{
    public class GuideEarningsSummaryDto
    {
        public int GuideId { get; set; }

        public int TotalPayments { get; set; }
        public int PaidPayments { get; set; }
        public int PendingPayments { get; set; }

        public decimal TotalGuideAmount { get; set; }
        public decimal TotalPlatformFeeFromGuide { get; set; }
        public decimal TotalUserServiceCharge { get; set; }
        public decimal TotalGuideReceives { get; set; }

        public decimal PaidGuideReceives { get; set; }
        public decimal PendingGuideReceives { get; set; }
    }
}
