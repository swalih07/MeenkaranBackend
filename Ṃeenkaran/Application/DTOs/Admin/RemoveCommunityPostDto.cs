using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class RemoveCommunityPostDto
    {
        [Required]
        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;
    }
}
