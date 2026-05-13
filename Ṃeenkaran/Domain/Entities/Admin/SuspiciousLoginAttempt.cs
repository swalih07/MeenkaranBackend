namespace Ṃeenkaran.Domain.Entities.Admin
{
    public class SuspiciousLoginAttempt
    {
        public int Id { get; set; }


        public string Email { get; set; } = string.Empty;
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }

        public string Reason { get; set; } = string.Empty;

        public int FailedCount { get; set; } = 1;

        public bool IsResolved { get; set; } = false;
        public int? ResolvedByAdminId { get; set; }
        public DateTime? ResolvedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
