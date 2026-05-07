namespace Ṃeenkaran.Domain.Entities.User
{
    public class CatchPost
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string FishType { get; set; } = string.Empty;

        public string Location { get; set; }  = string.Empty;

        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    }
}
