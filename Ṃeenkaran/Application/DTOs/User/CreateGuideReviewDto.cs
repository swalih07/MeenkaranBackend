namespace Ṃeenkaran.Application.DTOs.User
{
    public class CreateGuideReviewDto
    {
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
