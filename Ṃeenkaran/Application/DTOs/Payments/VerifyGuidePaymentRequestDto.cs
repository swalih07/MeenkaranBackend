using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Payments
{
    public class VerifyGuidePaymentRequestDto
    {
        [Required]
        public string RazorpayOrderId { get; set; } = string.Empty;

        [Required]
        public string RazorpayPaymentId { get; set; } = string.Empty;

        [Required]
        public string RazorpaySignature { get; set; } = string.Empty;
    }
}
