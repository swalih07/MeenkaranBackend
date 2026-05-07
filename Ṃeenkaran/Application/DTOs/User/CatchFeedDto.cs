namespace Ṃeenkaran.Application.DTOs.User
{
    public class CatchFeedDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }  = string.Empty;

        public string UserProfileImage { get; set; } = string.Empty;

        public string ImageUrl { get; set; }  = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string FishType { get; set; }  = string.Empty;

        public string Location { get; set; }  = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
