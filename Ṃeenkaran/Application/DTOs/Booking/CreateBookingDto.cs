using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Booking
{
    public class CreateBookingDto
    {
        [Required]
        public int GuidePackageId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Range(1,20)]
        public int PersonsCount { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}
