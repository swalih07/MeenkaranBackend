using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Booking;
using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Domain.Enums;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class GuideBookingService:IGuideBookingService
    {
        private readonly AppDbContext _context;

        public GuideBookingService(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<string> BookGuideAsync(CreateGuideBookingDto dto)
        //{
        //    var booking = new GuideBooking
        //    {
        //        UserId = dto.UserId,
        //        GuideId = dto.GuideId,
        //        GuidePackageId = dto.GuidePackageId,
        //        BookingDate = dto.BookingDate,
        //        Status = BookingStatus.Pending
        //    };
        //    _context.GuideBookings.Add(booking);
        //    await _context.SaveChangesAsync();

        //    return "Guide booked successfully";
        //}
        public async Task<ApiResponse<string>>CreateBookingAsync(int userId,CreateBookingDto dto)
        {
            var package = await _context.GuidePackages
                .Include(x => x.Guide)
                .FirstOrDefaultAsync(x => x.Id == dto.GuidePackageId && x.IsActive);

            if(package == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Package not found",
                    StatusCode = 404
                };
            }
            var booking = new GuideBooking
            {
                UserId = userId,
                GuideId = package.GuideId,
                GuidePackageId = package.Id,
                BookingDate = dto.BookingDate,
                PersonCount = dto.PersonsCount,
                Notes = dto.Notes,
                Status = BookingStatus.Pending
            };
            await _context.GuideBookings.AddAsync(booking);

            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Booking Request sent successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<List<BookingRequestDto>>>GetGuideBookingRequestsAsync(int guideId)
        {
            var bookings=await _context.GuideBookings
                .Include(x=>x.User)
                .Include(x=>x.GuidePackage)
                .Where(x=>x.GuideId==guideId)
                .OrderByDescending(x=>x.CreatedAt)
                .Select(x=>new BookingRequestDto
                {
                    BookingId=x.Id,
                    UserName=x.User.Name,
                    UserEmail=x.User.Email,
                    BookingDate=x.BookingDate,
                    PersonsCount=x.PersonCount,
                    Notes=x.Notes,
                    Status=x.Status.ToString(),

                })
                .ToListAsync();

            return new ApiResponse<List<BookingRequestDto>>
            {
                Success = true,
                Message = "Booking requests fetched successfully",
                StatusCode = 200,
                Data = bookings
            };
        }
        public async Task<ApiResponse<string>>UpdateBookingStatusAsync(int guideId,int bookingId,UpdateBookingStatusDto dto)
        {
            var booking=await _context.GuideBookings.FirstOrDefaultAsync(
                x=>x.Id==bookingId && x.GuideId==guideId);

            if(booking == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Booking Not Found",
                    StatusCode = 404
                };
            }
            var status = dto.Status.Trim().ToLower();

            if(status == "accepted")
            {
                booking.Status = BookingStatus.Accepted;
            }
            else if(status == "rejected")
            {
                booking.Status = BookingStatus.Rejected;
            }
            else
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid status",
                    StatusCode = 400
                };
            }
            await _context.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = $"Booing {status} successfully",
                StatusCode = 200
            };
        }
    }
}
