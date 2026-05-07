using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Domain.Enums;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class TripBookingRequestService:ITripBookingRequestService
    {
        private readonly AppDbContext _context;

        public TripBookingRequestService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string>CreateRequestAsync(CreateTripBookingRequestDto dto)
        {
            var request = new TripBookingRequest
            {
                UserId = dto.UserId,
                GuideId = dto.GuideId,
                TripDate = dto.TripDate,
                TimeSlot = dto.TimeSlot,
                GearRequirements = dto.GearRequirements,
                Notes = dto.Notes,
                Status = BookingStatus.Pending
            };
            _context.TripBookingRequests.Add(request);
            await _context.SaveChangesAsync();
            return "Trip booking request Created";
        }

        public async Task<IEnumerable<TripBookingRequest>> GetUserBookingAsync(int userId)
        {
            return await _context.TripBookingRequests
                .Include(x => x.Guide)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.TripDate)
                .ToListAsync();
        }

        public async Task<TripBookingRequest?> GetBookingByIdAsync(int bookingId)
        {
            return await _context.TripBookingRequests
                .Include(x => x.Guide)
                .FirstOrDefaultAsync(x => x.Id == bookingId);
        }

        public async Task<string> CancelbookingAsync(int bookingId)
        {
            var booking = await _context.TripBookingRequests.FindAsync(bookingId);
            if (booking == null) return "Booking not found";

            booking.Status = BookingStatus.Cancelled;
            await _context.SaveChangesAsync();
            return "Booking cancelled successfully";
        }
    }
}
