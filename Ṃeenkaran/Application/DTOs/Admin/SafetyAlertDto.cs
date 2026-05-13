using Ṃeenkaran.Domain.Enums;

namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class SafetyAlertDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AlertAudience Audience { get; set; }
        public AlertSeverity Severity { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByAdminId { get; set; }
        public string? CreatedByAdminName { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
