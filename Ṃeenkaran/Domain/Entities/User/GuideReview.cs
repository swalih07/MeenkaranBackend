namespace Ṃeenkaran.Domain.Entities.User
{
    public class GuideReview
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GuideId { get; set; }

        public int BookingId { get; set; }

        public int Rating { get; set; } // 1 to 5

        public string Comment { get; set; } = string.Empty;

        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
