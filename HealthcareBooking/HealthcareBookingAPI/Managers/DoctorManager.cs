using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.Interfaces;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBookingAPI.Managers
{
    public class DoctorManager : IDoctorManager
    {
        private HealthcareContext _context;
        public DoctorManager(HealthcareContext context)
        {
            _context = context;
        }
        public async Task<List<Doctor>> GetAllDoctorsAsync(int? numDoctors = null)
        {
            IQueryable<Doctor> query = _context.Doctors;

            if (numDoctors.HasValue && numDoctors.Value > 0)
            {
                query = query.Take(numDoctors.Value);
            }
            query.Include(o => o.SupportedBookingTypes);
            return await query.ToListAsync();
        }


        public async Task<Doctor> GetDoctorByIdAsync(Guid id)
        {
            if (_context.Doctors.Any())
            {
                return await _context.Doctors.Include(o => o.SupportedBookingTypes).FirstOrDefaultAsync(o => o.StaffId == id);
            }
            else
            {
                throw new Exception("not found with that id");
            }
        }

        public Task<Doctor> GetDoctorByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception("not found with that name");

            if (_context.Doctors.Any())
            {
                return _context.Doctors.Include(o => o.SupportedBookingTypes).FirstOrDefaultAsync(o => o.Name == name);
            }
            else
            {
                throw new Exception("gj");
            }
        }
    }
}
