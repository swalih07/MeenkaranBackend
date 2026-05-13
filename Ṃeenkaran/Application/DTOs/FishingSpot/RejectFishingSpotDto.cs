using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.FishingSpot
{
    public class RejectFishingSpotDto
    {
        [Required]
        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;
    }
}
