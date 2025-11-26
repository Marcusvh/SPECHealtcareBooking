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
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<NotifyStaff>> GetAllNotificationsByStaffIdAsync(Guid staffId)
        {
            return await _context.NotifyStaffs
                .AsNoTracking()
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
    }
}
