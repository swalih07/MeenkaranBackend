namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class ReportedCommunityPostDto
    {
        public int PostId { get; set; }
        public int PostUserId { get; set; }
        public string PostUserName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public int ReportCount { get; set; }
        public string? LatestReason { get; set; }

        public bool IsActive { get; set; }
        public bool IsRemoved { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
