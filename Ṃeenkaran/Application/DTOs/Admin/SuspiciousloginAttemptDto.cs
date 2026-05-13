namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class SuspiciousloginAttemptDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int FailedCount { get; set; }
        public bool IsResolved { get; set; }
        public int? ResolveByAdminId { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
