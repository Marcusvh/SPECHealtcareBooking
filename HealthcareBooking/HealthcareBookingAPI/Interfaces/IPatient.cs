using HealthcareModels.Models;

namespace HealthcareBookingAPI.Interfaces
{
    public interface IPatient
    {
        Task<List<Patient>> GetAllPatients();
    }
}
