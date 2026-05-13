using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Payments
{
    public class UpdatePlatformPaymentSettingRequestDto
    {
        [Range(0, 100)]
        public decimal GuidePlatformFeePercent { get; set; } = 20m;

        [Range(0, 100000)]
        public decimal UserServiceCharge { get; set; } = 15m;
    }
}
