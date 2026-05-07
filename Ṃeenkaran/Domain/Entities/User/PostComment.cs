namespace Ṃeenkaran.Domain.Entities.User
{
    public class PostComment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CatchPostId { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
