using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Reports
{
    public class CreateGuideFeedbackDto
    {
        [Required]
        public int GuideBookinId { get; set; }

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }


        public string Comment { get; set; } = string.Empty;
    }
}
