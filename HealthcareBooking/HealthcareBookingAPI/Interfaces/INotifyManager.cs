using HealthcareBookingAPI.DTO;
using HealthcareModels.Models.HealthcareStaff;

namespace HealthcareBookingAPI.Interfaces
{
    public interface INotifyManager
    {
        Task<ResultResponse<NotifyStaff>> CreateNotificationForStaffAsync(NotifyStaff notifyStaff);
        Task<List<NotifyStaff>> GetAllNotificationsByStaffIdAsync(Guid staffId);
        Task<List<NotifyStaff>> GetAllNotificationsAsync();
    }
}
