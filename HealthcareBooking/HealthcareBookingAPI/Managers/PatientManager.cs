using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.Interfaces;
using HealthcareModels.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBookingAPI.Managers
{
    public class PatientManager : IPatient
    {
        private HealthcareContext _context;

        public PatientManager(HealthcareContext context)
        {
           _context = context; 
        }
        public async Task<List<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }
    }
}
