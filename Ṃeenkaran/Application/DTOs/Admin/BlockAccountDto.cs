using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class BlockAccountDto
    {
        [Required]
        public string Reason { get; set; } = string.Empty;
    }
}
