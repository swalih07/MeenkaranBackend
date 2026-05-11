using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class RejectedGuideDto
    {
        [Required]
        public string Reason { get; set; } = string.Empty;
    }
}
