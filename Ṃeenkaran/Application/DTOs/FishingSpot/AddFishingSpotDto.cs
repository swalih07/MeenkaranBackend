using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.FishingSpot
{
    public class AddFishingSpotDto
    {
        [Required]
        [StringLength(100,MinimumLength =2)]
        public string SpotName { get; set; }=string.Empty;

        [Required]
        [StringLength (500,MinimumLength =5)]
        public string Description {  get; set; }=string.Empty;

        [Required]
        [StringLength(200,MinimumLength =2)]
        public string LocationName {  get; set; }=string.Empty;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string SpotType { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsHotspot { get; set; } = true;
    }
}
