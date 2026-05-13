using Ṃeenkaran.Domain.Enums;

namespace Ṃeenkaran.Domain.Entities.Admin
{
    public class SafetyAlert
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public AlertAudience Audience { get; set; } = AlertAudience.All;
        public AlertSeverity Severity { get; set; } = AlertSeverity.Info;

        public bool IsActive { get; set; } = true;

        public int CreatedByAdminId { get; set; }
        public string? CreatedByAdminName { get; set; }

        public DateTime?ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
