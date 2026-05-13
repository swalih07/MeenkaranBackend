namespace Ṃeenkaran.Domain.Entities.Admin
{
    public class CommunityPostReport
    {
        public int Id { get; set; }


        public int CommunityPostId { get; set; }
        public CommunityPost CommunityPost { get; set; }

        public int ReporterUserId { get; set; }
        public Ṃeenkaran.Domain.Entities.User.User ReporterUser { get; set; } = null!;

        public string Reason { get; set; } = string.Empty;
        public string? Details { get; set; }

        public bool IsResolved { get; set; } = false;
        public int? ResolvedByAdminId { get; set; }
        public DateTime? ResolvedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
