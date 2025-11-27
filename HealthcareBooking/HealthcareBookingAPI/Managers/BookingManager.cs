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

        public async Task<ResultResponse<Booking>> CreateBookingAsync(BookingDTO booking)
        {
            if (booking == null)
                return ResultResponse<Booking>.Fail("Booking data must be provided.");

            // Validate BookingType
            var bType = await _context.BookingTypes
                .FindAsync(booking.BookingTypeId);

            if (bType == null)
                return ResultResponse<Booking>.Fail("Invalid Booking Type specified.");

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
                    return ResultResponse<Booking>.Fail("No available doctor supports this booking type.");

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
                    return ResultResponse<Booking>.Fail("Specified staff member does not exist.");

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

            return ResultResponse<Booking>.Success(newBooking);
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
        public async Task<List<Booking>> GetBookingsByStageAsync(BookingCheckStage stage)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(b => b.BookingCheckStage == stage)
                .ToListAsync();
        }
        public async Task<List<DetailedBookingViewDTO>> GetDetailedBookingViewsAsync()
        {
            var bookings = await _context.Bookings
                .AsNoTracking()
                .Select(ns => new DetailedBookingViewDTO()
                {
                    BookingStatus = ns.Status,
                    BookingStage = ns.BookingCheckStage,
                    BookingTypeName = ns.BookingType.Name,
                    StartTime = ns.StartTime,
                    PatientFullName = ns.Patient.FullName,
                    DOB = ns.Patient.DateOfBirth,
                    Duration = new TimeOnly(0, 30),
                    PatientNotes = ns.PatientNotes,
                    StaffNotes = ns.StaffNotes,
                    DetailedBookingViewid = ns.BookingId,
                    PatientEmail = ns.Patient.Email,
                    PatientPhoneNumber = ns.Patient.Phone
                })
                .OrderBy(o => o.StartTime)
                .ToListAsync();
            return bookings;
        }
        public async Task<ResultResponse<Guid>> UpdateStaffNoteOnBooking(Guid bookingId, string staffNote)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (booking == null)
            {
                return ResultResponse<Guid>.Fail($"Booking with ID {bookingId} not found.");
            }
            booking.StaffNotes = staffNote;
            booking.UpdatedAt = DateTime.UtcNow;
            try
            {
                await _context.SaveChangesAsync();
                return ResultResponse<Guid>.Success(booking.BookingId);
            }
            catch (Exception ex)
            {
                return ResultResponse<Guid>.Fail($"Error updating booking: {ex.Message}");
            }
        }
        public async Task<ResultResponse<Guid>> ConfirmBookingByStaffAsync(Guid bookingId, Guid staffId)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == bookingId && b.StaffId == staffId);
            if (booking == null)
            {
                return ResultResponse<Guid>.Fail($"Booking with ID {bookingId} not found for staff ID {staffId}.");
            }
            booking.BookingCheckStage = BookingCheckStage.Confirmed;
            booking.StaffConfirmedAt = DateTime.UtcNow;
            booking.ConfirmedByStaffId = staffId;
            booking.UpdatedAt = DateTime.UtcNow;
            try
            {
                await _context.SaveChangesAsync();

                return ResultResponse<Guid>.Success(booking.PatientId); // to use for notify patient
            }
            catch (Exception ex)
            {
                return ResultResponse<Guid>.Fail($"Error confirming booking: {ex.Message}");
            }
        }
        public async Task<ResultResponse<Guid>> RejectBookingByStaffAsync(Guid bookingId, Guid staffId, string? reason)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == bookingId && b.StaffId == staffId);
            if (booking == null)
            {
                return ResultResponse<Guid>.Fail($"Booking with ID {bookingId} not found for staff ID {staffId}.");
            }
            booking.BookingCheckStage = BookingCheckStage.Cancelled;
            booking.Status = BookingStatus.Cancelled;
            booking.StaffConfirmedAt = DateTime.UtcNow; // should probably be StaffRejectedAt but keeping consistent with existing field
            booking.ConfirmedByStaffId = staffId; // should probably be RejectedByStaffId but keeping consistent with existing field
            booking.StaffNotes = booking.StaffNotes + "\n\nReason for rejection:\n" + reason;
            booking.UpdatedAt = DateTime.UtcNow;
            try
            {
                await _context.SaveChangesAsync();

                return ResultResponse<Guid>.Success(booking.PatientId); // to use for notify patient
            }
            catch (Exception ex)
            {
                return ResultResponse<Guid>.Fail($"Error rejecting booking: {ex.Message}");
            }
        }
        
    }
}
