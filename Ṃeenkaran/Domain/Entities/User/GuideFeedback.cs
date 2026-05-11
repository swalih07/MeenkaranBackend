namespace Ṃeenkaran.Domain.Entities.User
{
    public class GuideFeedback
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public Guide Guide { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int GuideBookingId { get; set; }
        public GuideBooking GuideBooking { get; set; }=null!;

        public int Rating {  get; set; }
        public string Comment { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
