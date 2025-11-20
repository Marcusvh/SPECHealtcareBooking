using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.DTO;
using HealthcareBookingAPI.Helpers.DTOMappers;
using HealthcareBookingAPI.Interfaces;
using HealthcareModels.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBookingAPI.Managers
{
    public class BookingManager : IBookingManager
    {
        private HealthcareContext _context;
        public BookingManager(HealthcareContext context)
        {
            _context = context;
        }

        public async Task<string> CreateBooking(BookingDTO booking)
        {
            //------------------
            //-------------------
            // split into staff and doctor
            try
            {
                Booking newBooking = BookingMapper.MapBooking(booking);

                // staffid
                if (booking.StaffId.HasValue) {
                    BookingType bType = await _context.BookingTypes.FirstOrDefaultAsync(o => o.BookingTypeId == booking.BookingTypeId);
                    await _context.Doctors.FirstOrDefaultAsync(o => o.SupportedBookingTypes.Contains(bType));
                } 
                else {
                    newBooking.StaffId = booking.StaffId.Value;
                }
                newBooking.BookingCheckStage = BookingCheckStage.First;
                newBooking.CreatedAt = DateTime.UtcNow;
                newBooking.Status = BookingStatus.Scheduled;
                await _context.Bookings.AddAsync(newBooking);
                await _context.SaveChangesAsync();
                return "yay";
            }
            catch (Exception)
            {
                return "nay";
            }


        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<List<BookingType>> GetAllBookingTypesAsync()
        {
            return await _context.BookingTypes.ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(Guid id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(o => o.BookingId == id);
        }

        public async Task<BookingType> GetBookingTypeByIdAsync(Guid id)
        {
            if (_context.BookingTypes.Any())
            {
                return await _context.BookingTypes.FirstOrDefaultAsync(o => o.BookingTypeId == id);
            }
            else
            {
                throw new Exception("not found with that id");
            }
        }

        public Task<BookingType> GetBookingTypeByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception("not found with that name");

            if (_context.BookingTypes.Any())
            {
                return _context.BookingTypes.FirstOrDefaultAsync(o => o.Name == name);
            }
            else
            {
                throw new Exception("gj");
            }
        }
    }
}
