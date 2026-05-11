namespace Ṃeenkaran.Application.DTOs.Booking
{
    public class BookingRequestDto
    {
        public int BookingId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public string PackageName { get; set; } = string.Empty;

        public DateTime BookingDate { get; set; }

        public int PersonsCount { get; set; }

        public string Notes { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
