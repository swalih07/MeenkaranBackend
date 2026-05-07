namespace Ṃeenkaran.Application.DTOs.User
{
    public class GuideReviewDto
    {
        public int ReviewId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserProfileImage { get; set; }= string.Empty;

        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
