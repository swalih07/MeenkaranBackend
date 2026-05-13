using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Payments
{
    public class CreateGuidePaymentOrderRequestDto
    {
        [Required]
        public int GuideId { get; set; }

        [Range(1,1000000)]
        public decimal GuideAmount { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
