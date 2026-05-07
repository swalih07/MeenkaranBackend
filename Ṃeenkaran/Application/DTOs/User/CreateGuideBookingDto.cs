namespace Ṃeenkaran.Application.DTOs.User
{
    public class CreateGuideBookingDto
    {
        public int UserId { get; set; }

        public int GuideId { get; set; }

        public int GuidePackageId { get; set; }

        public DateTime BookingDate { get; set; }
    }
}
