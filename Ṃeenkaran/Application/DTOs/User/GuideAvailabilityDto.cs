namespace Ṃeenkaran.Application.DTOs.User
{
    public class GuideAvailabilityDto
    {
        public int GuideId { get; set; }

        public string GuideName { get; set; } = string.Empty;

        public DateTime AvailableDate { get; set; }

        public string TimeSlot { get; set; } = string.Empty;

        public bool IsBooked { get; set; }

        public List<GuidePackageDto> Packages { get; set; }
            = new();
    }
}
