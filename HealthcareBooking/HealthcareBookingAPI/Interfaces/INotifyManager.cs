using HealthcareBookingAPI.DTO;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;

namespace HealthcareBookingAPI.Interfaces
{
    public interface INotifyManager
    {
        Task<ResultResponse<NotifyStaff>> CreateNotificationForStaffAsync(NotifyStaff notifyStaff);
        Task<List<NotifyStaff>> GetAllNotificationsByStaffIdAsync(Guid staffId);
        Task<List<NotifyStaff>> GetAllNotificationsAsync();
        Task<ResultResponse<NotifyStaff>> UpdateNotificationStatusAsync(Guid notifyStaffId);
        Task<ResultResponse<NotifyPatient>> CreateNotificationForPatientAsync(NotifyPatient notifyPatient);
        Task UpdateNotityStaffNeedsActionAsync(Guid bookingId, bool needsAction);
    }
}
