using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.DTO;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HealthcareBookingAPI.DataGeneration
{
    public class DataGenerationFacade
    {
        private readonly HealthcareContext _context;
        private StaffDataGeneration _staffDataGeneration;
        private readonly PatientDataGeneration _patientDataGeneration;
        private readonly LocationDataGeneration _locationDataGeneration;
        private readonly BookingDataGeneration _bookingDataGeneration;
        public DataGenerationFacade(HealthcareContext context)
        {
            _context = context;
            
            _patientDataGeneration = new(context);
            _bookingDataGeneration = new(context);
            _locationDataGeneration = new();
        }
        public async Task GenerateStaffPatientsLocations(DataGenerationStaffPatientsLocationsDTO dto) 
        {
            _staffDataGeneration = new(_context);
            List<Location> locations = _locationDataGeneration.GenerateLocation().Generate(dto.NumLocation);
            await _context.Locations.AddRangeAsync(locations);
            await _context.SaveChangesAsync();

            List<Patient> patients = _patientDataGeneration.GeneratePatient(dto.FixedLocationId).Generate(dto.NumPatient);
            List<Staff> staffs = _staffDataGeneration.GenerateStaff().Generate(dto.NumStaff);

            List<Doctor> doctors = _staffDataGeneration.GenerateDoctor().Generate(dto.NumDoctor);
            await _context.Doctors.AddRangeAsync(doctors);
            await _context.SaveChangesAsync();

            // Now attach BookingTypes to the join table
            foreach (var doct in doctors)
            {
                doct.SupportedBookingTypes = await _context.BookingTypes
               .Where(bt => doct.SupportedBookingTypeIds.Contains(bt.BookingTypeId)).ToListAsync();
            }
            
            await _context.SaveChangesAsync();


            List<Nurse> nurses = _staffDataGeneration.GenerateNurse().Generate(dto.NumNurse);
            await _context.Nurses.AddRangeAsync(nurses);
            await _context.SaveChangesAsync();

            // Now attach BookingTypes to the join table
            foreach (var nurs in nurses)
            {
                nurs.SupportedBookingTypes = await _context.BookingTypes
               .Where(bt => nurs.SupportedBookingTypeIds.Contains(bt.BookingTypeId)).ToListAsync();
            }

            List<Guid> doctorIds = _context.Doctors.Select(o => o.StaffId).ToList();
            List<MedicalStudent> medStudent = _staffDataGeneration.GenerateMedStudent(doctorIds).Generate(dto.NumMedStudent);
            await _context.MedicalStudents.AddRangeAsync(medStudent);
            await _context.SaveChangesAsync();

            // Now attach BookingTypes to the join table
            foreach (var medStu in medStudent)
            {
                medStu.SupportedBookingTypes = await _context.BookingTypes
               .Where(bt => medStu.SupportedBookingTypeIds.Contains(bt.BookingTypeId)).ToListAsync();
            }

            await _context.Patients.AddRangeAsync(patients);
            await _context.Staffs.AddRangeAsync(staffs);
            await _context.SaveChangesAsync();
        }
        public async Task GenerateBookings(DataGenerationBookingsDTO dto) 
        {
            List<BookingType> bookingTypes = _bookingDataGeneration.GenerateBookingType(dto.NumBookingType);
            List<Booking> bookings = _bookingDataGeneration.GenerateBooking().Generate(dto.NumBooking);

            await _context.BookingTypes.AddRangeAsync(bookingTypes);
            await _context.Bookings.AddRangeAsync(bookings);

            await _context.SaveChangesAsync();
        }
        public async Task GenerateAllData(DataGenerationAllDTO dto)
        {
            DataGenerationStaffPatientsLocationsDTO splDTO = new()
            {
                FixedLocationId = dto.FixedLocationId,
                NumDoctor = dto.NumDoctor,
                NumLocation = dto.NumLocation,
                NumNurse = dto.NumNurse,
                NumMedStudent = dto.NumMedStudent,
                NumPatient = dto.NumPatient,
                NumStaff = dto.NumStaff,
            };

            List<BookingType> bookingTypes = _bookingDataGeneration.GenerateBookingType(dto.NumBookingType);
            await _context.BookingTypes.AddRangeAsync(bookingTypes);
            await _context.SaveChangesAsync();

            await GenerateStaffPatientsLocations(splDTO);

            List<Booking> bookings = _bookingDataGeneration.GenerateBooking().Generate(dto.NumBooking);
            await _context.Bookings.AddRangeAsync(bookings);
            await _context.SaveChangesAsync();
        }
    }
}
