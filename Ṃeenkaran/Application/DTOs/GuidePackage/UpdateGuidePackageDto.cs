using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.GuidePackage
{
    public class UpdateGuidePackageDto
    {
        [Required]
        [StringLength(100)]
        public string PackageName { get; set; } = string.Empty;


        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(1,100000)]
        public decimal Price { get; set; }

        [Range(1, 72)]
        public int DurationHours { get; set; }

        [Required]
        public string Includes { get; set; } = string.Empty;

        [Required]
        public string TripLocation { get; set; } = string.Empty;


    }
}
