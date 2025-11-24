using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.DTO;
using HealthcareBookingAPI.Helpers.DTOMappers;
using HealthcareBookingAPI.Interfaces;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
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

        #region Booking Creation

        public async Task<ResultResponse<Guid>> CreateBookingAsync(BookingDTO booking)
        {
            if (booking == null)
                return ResultResponse<Guid>.Fail("Booking data must be provided.");

            // Validate BookingType
            var bType = await _context.BookingTypes
                .FindAsync(booking.BookingTypeId);

            if (bType == null)
                return ResultResponse<Guid>.Fail("Invalid Booking Type specified.");

            // Map DTO to entity
            var newBooking = BookingMapper.MapBooking(booking);

            // Assign Staff / Doctor
            Doctor? doctor;

            if (!booking.StaffId.HasValue)
            {
                // Pick a doctor who supports the booking type
                doctor = await _context.Staffs
                    .OfType<Doctor>()
                    .Where(d => d.SupportedBookingTypes
                        .Any(bt => bt.BookingTypeId == booking.BookingTypeId))
                    .FirstOrDefaultAsync();

                if (doctor == null)
                    return ResultResponse<Guid>.Fail("No available doctor supports this booking type.");

                newBooking.StaffId = doctor.StaffId;
                newBooking.Staff = doctor;
            }
            else
            {
                // Validate staff exists and is a doctor
                doctor = await _context.Staffs
                    .OfType<Doctor>()
                    .FirstOrDefaultAsync(d => d.StaffId == booking.StaffId.Value);

                if (doctor == null)
                    return ResultResponse<Guid>.Fail("Specified staff member does not exist.");

                newBooking.StaffId = doctor.StaffId;
                newBooking.Staff = doctor;
            }

            // Metadata
            newBooking.BookingCheckStage = BookingCheckStage.First;
            newBooking.CreatedAt = DateTime.UtcNow;
            newBooking.Status = BookingStatus.Scheduled;

            // Save
            await _context.Bookings.AddAsync(newBooking);
            await _context.SaveChangesAsync();

            return ResultResponse<Guid>.Success(newBooking.BookingId);
        }

        #endregion

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<BookingType>> GetAllBookingTypesAsync()
        {
            return await _context.BookingTypes
                .AsNoTracking()
                .GroupBy(bt => bt.Name)
                .Select(g => g.First())
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid id)
        {
            return await _context.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<BookingType?> GetBookingTypeByIdAsync(Guid id)
        {
            return await _context.BookingTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(bt => bt.BookingTypeId == id);
        }

        public async Task<ResultResponse<BookingType>> GetBookingTypeByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                ResultResponse<BookingType>.Fail("Booking type name must be provided.");

            var resultBookngType = await _context.BookingTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(bt => bt.Name.ToLower() == name.ToLower());

            return ResultResponse<BookingType>.Success(resultBookngType);
        }

    }
}
