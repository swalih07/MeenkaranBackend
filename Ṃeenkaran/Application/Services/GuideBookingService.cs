using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Domain.Enums;
using Ṃeenkaran.Infrastructure.Data;

namespace Ṃeenkaran.Application.Services
{
    public class GuideBookingService:IGuideBookingService
    {
        private readonly AppDbContext _context;

        public GuideBookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> BookGuideAsync(CreateGuideBookingDto dto)
        {
            var booking = new GuideBooking
            {
                UserId = dto.UserId,
                GuideId = dto.GuideId,
                GuidePackageId = dto.GuidePackageId,
                BookingDate = dto.BookingDate,
                Status = BookingStatus.Pending
            };
            _context.GuideBookings.Add(booking);
            await _context.SaveChangesAsync();

            return "Guide booked successfully";
        }
    }
}
