using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.DTO;
using HealthcareBookingAPI.Interfaces;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBookingAPI.Managers
{
    public class NotifyManager : INotifyManager
    {
        private readonly HealthcareContext _context;
        public NotifyManager(HealthcareContext context)
        {
            _context = context;
        }
        public async Task<ResultResponse<NotifyStaff>> CreateNotificationForStaffAsync(NotifyStaff notifyStaff)
        {
            if (notifyStaff == null)
                return ResultResponse<NotifyStaff>.Fail("The notify content may not be null");

            Staff? staff = await _context.Staffs.FindAsync(notifyStaff.StaffId);
            if (staff == null)
                return ResultResponse<NotifyStaff>.Fail($"Could not find the staff with given ID: {notifyStaff.StaffId}");

            Booking? booking = await _context.Bookings.FindAsync(notifyStaff.RelatedBookingId);
            if (booking == null)
                return ResultResponse<NotifyStaff>.Fail($"Could not find the booking with given ID: {notifyStaff.RelatedBookingId}");

            booking.BookingCheckStage = BookingCheckStage.Second;

            _context.NotifyStaffs.Add(notifyStaff);
            await _context.SaveChangesAsync();
            return ResultResponse<NotifyStaff>.Success(notifyStaff);
        }

        public async Task<List<NotifyStaff>> GetAllNotificationsAsync()
        {
            return await _context.NotifyStaffs
                .AsNoTracking()
                .Where(o => o.NeedsAction == true)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<NotifyStaff>> GetAllNotificationsByStaffIdAsync(Guid staffId)
        {
            return await _context.NotifyStaffs
                .AsNoTracking()
                .Where(o => o.NeedsAction == true)
                .OrderBy(o => o.CreatedAt)
                .Where(n => n.StaffId == staffId)
                .ToListAsync();
        }
        public async Task<ResultResponse<NotifyStaff>> UpdateNotificationStatusAsync(Guid notifyStaffId)
        {
            NotifyStaff? notifyStaff = await _context.NotifyStaffs.FindAsync(notifyStaffId);

            if (notifyStaff == null)
                return ResultResponse<NotifyStaff>.Fail($"Could not find the notification with given ID: {notifyStaffId}");

            notifyStaff.NotificationStatus = NotificationStatus.Read;
            _context.NotifyStaffs.Update(notifyStaff);
            await _context.SaveChangesAsync();
            return ResultResponse<NotifyStaff>.Success(notifyStaff);
        }
        public async Task<ResultResponse<NotifyPatient>> CreateNotificationForPatientAsync(NotifyPatient notifyPatient)
        {
            if (notifyPatient == null)
                return ResultResponse<NotifyPatient>.Fail("The notify content may not be null");

            Patient? patient = await _context.Patients.FindAsync(notifyPatient.PatientId);
            if (patient == null)
                return ResultResponse<NotifyPatient>.Fail($"Could not find the patient with given ID: {notifyPatient.PatientId}");

            Booking? booking = await _context.Bookings.FindAsync(notifyPatient.BookingId);
            if (booking == null)
                return ResultResponse<NotifyPatient>.Fail($"Could not find the booking with given ID: {notifyPatient.BookingId}");

            _context.NotifyPatients.Add(notifyPatient);
            await _context.SaveChangesAsync();
            return ResultResponse<NotifyPatient>.Success(notifyPatient);
        }
        public async Task UpdateNotityStaffNeedsActionAsync(Guid bookingId, bool needsAction) // TODO: feels to static 
        {
            List<NotifyStaff> notifications = await _context.NotifyStaffs
                .Where(n => n.RelatedBookingId == bookingId)
                .ToListAsync();
            foreach (var notification in notifications)
            {
                notification.NeedsAction = needsAction;
            }
            await _context.SaveChangesAsync();
        }
    }
}
