namespace Ṃeenkaran.Domain.Entities.User
{
    public class PostLike
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CatchPostId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
