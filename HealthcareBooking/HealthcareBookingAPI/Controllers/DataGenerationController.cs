using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.DataGeneration;
using HealthcareBookingAPI.DTO;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataGenerationController : ControllerBase
    {
        private readonly HealthcareContext _context;
        private readonly PatientDataGeneration _patientDataGeneration;
        private readonly LocationDataGeneration _locationDataGeneration;
        private readonly StaffDataGeneration _staffDataGeneration;
        private readonly BookingDataGeneration _bookingDataGeneration;
        private readonly DataGenerationFacade _dataGenerationFacade;
        public DataGenerationController(HealthcareContext context)
        {
            _context = context;
            _patientDataGeneration = new(context);
            _staffDataGeneration = new(context);
            _bookingDataGeneration = new(context);
            _locationDataGeneration = new();
            _dataGenerationFacade = new(context);
        }
        // GET: api/<DataGenerationController>
        [HttpPost("patients")]
        public async Task<string> GeneratePatients([FromBody] int amount = 1)
        {
            List<Patient> patients = _patientDataGeneration.GeneratePatient().Generate(amount);
            _context.Patients.AddRange(patients);
            await _context.SaveChangesAsync();
            return $"Generated {amount} patients";
        }

        // GET api/<DataGenerationController>/5
        [HttpPost("location")]
        public async Task<string> GenerateLocations([FromBody] int amount = 1)
        {
            List<Location> locations = _locationDataGeneration.GenerateLocation().Generate(amount);
            _context.Locations.AddRange(locations);
            await _context.SaveChangesAsync();
            return $"Generated {amount} locations";
        }

        // POST api/<DataGenerationController>
        [HttpPost("staff")]
        public async Task<string> GenerateStaff([FromBody] int amount = 1)
        {
            List<Staff> staffs = _staffDataGeneration.GenerateStaff().Generate(amount);
            _context.Staffs.AddRange(staffs);
            await _context.SaveChangesAsync();
            return $"Generated {amount} staffs";
        }

        [HttpPost("doctor")]
        public async Task<string> GenerateDoctor([FromBody] int amount = 1)
        {
            List<Doctor> doctor = _staffDataGeneration.GenerateDoctor().Generate(amount);
            _context.Doctors.AddRange(doctor);
            await _context.SaveChangesAsync();
            return $"Generated {amount} doctor";
        }

        // DELETE api/<DataGenerationController>/5
        [HttpPost("nurse")]
        public async Task<string> GenerateNurse([FromBody] int amount = 1)
        {
            List<Nurse> nurse = _staffDataGeneration.GenerateNurse().Generate(amount);
            _context.Nurses.AddRange(nurse);
            await _context.SaveChangesAsync();
            return $"Generated {amount} nurse";
        }
        [HttpPost("medStudent")]
        public async Task<string> GenerateMedStudent([FromBody] int amount = 1)
        {
            List<Guid> doctorIds = _context.Doctors.Select(o => o.StaffId).ToList();
            List<MedicalStudent> medStudent= _staffDataGeneration.GenerateMedStudent(doctorIds).Generate(amount);
            _context.MedicalStudents.AddRange(medStudent);
            await _context.SaveChangesAsync();
            return $"Generated {amount} medical students";
        }
        [HttpPost("bookingType")]
        public async Task<string> GenerateBookingType([FromBody] int amount = 1)
        {
            // works a bit to wild
            List<BookingType> bookingType = _bookingDataGeneration.GenerateBookingType(amount);
            _context.BookingTypes.AddRange(bookingType);
            await _context.SaveChangesAsync();
            return $"Generated {amount} booking types";
        }
        [HttpPost("booking")]
        public async Task<string> GenerateBooking([FromBody] int amount = 1)
        {
            List<Booking> booking = _bookingDataGeneration.GenerateBooking().Generate(amount);
            _context.Bookings.AddRange(booking);
            await _context.SaveChangesAsync();
            return $"Generated {amount} booking";
        }
        [HttpPost("staffPatientsLocations")]
        public async Task<string> GenerateStaffPatientsLocations([FromBody] DataGenerationStaffPatientsLocationsDTO dto)
        {
            await _dataGenerationFacade.GenerateStaffPatientsLocations(dto);

            return
                $"Generated:\n" +
                $"- {dto.NumLocation} locations\n" +
                $"- {dto.NumPatient} patients\n" +
                $"- {dto.NumDoctor + dto.NumNurse + dto.NumMedStudent} staff (Doctors: {dto.NumDoctor}, Nurses: {dto.NumNurse}, MedStudents: {dto.NumMedStudent})\n" +
                (dto.FixedLocationId != null
                    ? $"- Fixed location assigned: {dto.FixedLocationId}"
                    : "- No fixed location ID provided");
        }
        [HttpPost("allBookings")]
        public async Task<string> GenerateBookings([FromBody] DataGenerationBookingsDTO dto)
        {
            await _dataGenerationFacade.GenerateBookings(dto);

            return
                "Generated:\n" +
                $"- {dto.NumBookingType} booking types\n " +
                $"- {dto.NumBooking} bookings";
        }
        [HttpPost("allData")]
        public async Task<string> GenerateAllData([FromBody] DataGenerationAllDTO dto)
        {
            await _dataGenerationFacade.GenerateAllData(dto);

            return
                $"Generated:\n" +
                $"- {dto.NumBookingType} booking types\n" +
                $"- {dto.NumBooking} bookings\n" +
                $"- {dto.NumLocation} locations\n" +
                $"- {dto.NumPatient} patients\n" +
                $"- {dto.NumDoctor + dto.NumNurse + dto.NumMedStudent} staff (Doctors: {dto.NumDoctor}, Nurses: {dto.NumNurse}, MedStudents: {dto.NumMedStudent})\n" +
                (dto.FixedLocationId != null
                    ? $"- Fixed location assigned: {dto.FixedLocationId}"
                    : "- No fixed location ID provided");
        }
    }
}
