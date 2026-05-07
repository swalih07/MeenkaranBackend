namespace Ṃeenkaran.Domain.Entities.User
{
    public class GuideAvailability
    {
        public int Id { get; set; }

        public int GuideId { get; set; }

        public Guide Guide { get; set; }

        public DateTime AvailableDate { get; set; }

        public string TimeSlot { get; set; } = string.Empty;

        public bool IsBooked { get; set; }
    }
}
