namespace Ṃeenkaran.Domain.Entities.User
{
    public class PlatformPaymentSetting
    {
        public int Id { get; set; }

        public decimal GuidePlatformFeePercent { get; set; } = 20m;
        public decimal UserServiceCharge { get; set; } = 15m;

        public bool IsActive { get; set; } = true;

        public int UpdatedByAdminId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
