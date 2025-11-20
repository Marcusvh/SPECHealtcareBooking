using HealthcareModels.Models.HealthcareStaff;

namespace HealthcareBookingAPI.Interfaces
{
    public interface IDoctorManager
    {
        Task<List<Doctor>> GetAllDoctorsAsync(int? numDoctors);
        Task<Doctor> GetDoctorByIdAsync(Guid id);
        Task<Doctor> GetDoctorByNameAsync(string name);
    }
}
