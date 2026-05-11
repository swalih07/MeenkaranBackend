using System.ComponentModel.DataAnnotations;

namespace Ṃeenkaran.Application.DTOs.Booking
{
    public class UpdateBookingStatusDto
    {
        [Required]
        public string Status { get; set; }= string.Empty;
    }
}
