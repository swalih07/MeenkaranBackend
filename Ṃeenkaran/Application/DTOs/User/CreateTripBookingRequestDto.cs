namespace Ṃeenkaran.Application.DTOs.User
{
    public class CreateTripBookingRequestDto
    {

        public int UserId { get; set; }

        public int GuideId { get; set; }

        public DateTime TripDate { get; set; }

        public string TimeSlot { get; set; } = string.Empty;

        public string GearRequirements { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
    }
}
