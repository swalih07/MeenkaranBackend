namespace Ṃeenkaran.Domain.Entities.Admin
{
    public class CommunityPost
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Ṃeenkaran.Domain.Entities.User.User User { get; set; } = null!;


        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }


        public bool IsActive { get; set; } = true;
        public bool IsRemoved { get; set; } = false;


        public string? RemovalReason { get; set; }
        public int? ReviewedByAdmin { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? RemovedAt { get; set; }


        public ICollection<CommunityPostReport> Reports { get; set; } = new List<CommunityPostReport>();
    }
}
