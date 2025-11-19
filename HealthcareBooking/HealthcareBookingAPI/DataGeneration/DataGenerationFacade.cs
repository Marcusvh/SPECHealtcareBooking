using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.DTO;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;

namespace HealthcareBookingAPI.DataGeneration
{
    public class DataGenerationFacade
    {
        private readonly HealthcareContext _context;
        private readonly StaffDataGeneration _staffDataGeneration;
        private readonly PatientDataGeneration _patientDataGeneration;
        private readonly LocationDataGeneration _locationDataGeneration;
        private readonly BookingDataGeneration _bookingDataGeneration;
        public DataGenerationFacade(HealthcareContext context)
        {
            _context = context;
            _staffDataGeneration = new(context);
            _patientDataGeneration = new(context);
            _bookingDataGeneration = new(context);
            _locationDataGeneration = new();
        }
        public async void GenerateStaffPatientsLocations(DataGenerationStaffPatientsLocationsDTO dto) 
        {
            List<Location> locations = _locationDataGeneration.GenerateLocation().Generate(dto.NumLocation);
            List<Patient> patients = _patientDataGeneration.GeneratePatient(dto.FixedLocationId).Generate(dto.NumPatient);
            List<Staff> staffs = _staffDataGeneration.GenerateStaff().Generate(dto.NumStaff);
            List<Doctor> doctors = _staffDataGeneration.GenerateDoctor().Generate(dto.NumDoctor);
            List<Nurse> nurses = _staffDataGeneration.GenerateNurse().Generate(dto.NumNurse);
            List<MedicalStudent> medStudent = _staffDataGeneration.GenerateMedStudent().Generate(dto.NumMedStudent);

            _context.Locations.AddRange(locations);
            _context.Patients.AddRange(patients);
            _context.Staffs.AddRange(staffs);
            _context.Doctors.AddRange(doctors);
            _context.Nurses.AddRange(nurses);
            _context.MedicalStudents.AddRange(medStudent);
            await _context.SaveChangesAsync();
        }
        public void GenerateBookings(DataGenerationBookingsDTO dto) 
        {
            _bookingDataGeneration.GenerateBookingType().Generate(dto.NumBookingType);
            _bookingDataGeneration.GenerateBooking().Generate(dto.NumBooking);
        }
        public void GenerateAllData(DataGenerationAllDTO dto)
        {
            DataGenerationStaffPatientsLocationsDTO splDTO = new DataGenerationStaffPatientsLocationsDTO()
            {
                FixedLocationId = dto.FixedLocationId,
                NumDoctor = dto.NumDoctor,
                NumLocation = dto.NumLocation,
                NumNurse = dto.NumNurse,
                NumMedStudent = dto.NumMedStudent,
                NumPatient = dto.NumPatient,
                NumStaff = dto.NumStaff,
            };
            _bookingDataGeneration.GenerateBookingType().Generate(dto.NumBookingType);
            GenerateStaffPatientsLocations(splDTO);
            _bookingDataGeneration.GenerateBooking().Generate(dto.NumBooking);
        }
    }
}
