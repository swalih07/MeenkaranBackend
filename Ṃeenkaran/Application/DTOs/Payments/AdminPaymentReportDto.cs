namespace Ṃeenkaran.Application.DTOs.Payments
{
    public class AdminPaymentReportDto
    {
        public int TotalPayments { get; set; }
        public int PaidPayments { get; set; }
        public int PendingPayments { get; set; }
        public int FailedPayments { get; set; }

        public decimal TotalGuideAmount { get; set; }
        public decimal TotalPlatformFeeFromGuide { get; set; }
        public decimal TotalUserServiceCharge { get; set; }
        public decimal TotalUserPays { get; set; }
        public decimal TotalGuideReceives { get; set; }

        public decimal TotalPaidAmount { get; set; }
        public decimal TotalPendingAmount { get; set; }
    }
}
