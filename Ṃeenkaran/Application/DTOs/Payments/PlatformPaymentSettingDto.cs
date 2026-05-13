namespace Ṃeenkaran.Application.DTOs.Payments
{
    public class PlatformPaymentSettingDto
    {
        public int Id { get; set; }

        public decimal GuidePlatformFeePercent { get; set; }
        public decimal UserServiceCharge { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedByAdminId { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
