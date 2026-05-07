namespace Ṃeenkaran.Application.DTOs.User
{
    public class CreateLikeDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
    }

    public class CreateCommentDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
