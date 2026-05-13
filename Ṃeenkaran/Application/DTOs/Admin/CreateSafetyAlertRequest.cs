using Ṃeenkaran.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class CreateSafetyAlertRequest
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;


        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;


        [Required]
        public AlertAudience Audience { get; set; } = AlertAudience.All;

        [Required]
        public AlertSeverity Severity { get; set; } = AlertSeverity.Warning;

        public DateTime? ExpiresAt { get; set; }
    }
}
