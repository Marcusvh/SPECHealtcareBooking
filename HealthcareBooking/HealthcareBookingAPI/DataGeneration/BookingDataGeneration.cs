using Bogus;
using HealthcareBookingAPI.Context;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;

namespace HealthcareBookingAPI.DataGeneration
{
    public class BookingDataGeneration
    {
        private readonly HealthcareContext _context;

        public BookingDataGeneration(HealthcareContext context)
        {
            _context = context;
        }
        public Faker<BookingType> GenerateBookingType()
        {
            return new Faker<BookingType>()
                .RuleFor(bt => bt.BookingTypeId, f => Guid.NewGuid())
                .RuleFor(bt => bt.Name, f => f.Commerce.ProductName())
                .RuleFor(bt => bt.Description, f => f.Lorem.Sentence());
        }
        public Faker<Booking> GenerateBooking()
        {
            // Load related entities from DB
            List<Patient> patients = _context.Patients.ToList();
            List<Staff> staffMembers = _context.Staffs.ToList();
            List<BookingType> bookingTypes = _context.BookingTypes.ToList();

            var faker = new Faker<Booking>("nb_NO");

            return faker
                .RuleFor(b => b.BookingId, f => Guid.NewGuid())
                .RuleFor(b => b.StartTime, f => f.Date.Future().ToUniversalTime())
                .RuleFor(b => b.EndTime, (f, b) => b.StartTime.AddHours(f.Random.Int(1, 3))) // 1-3 hour appointments
                .RuleFor(b => b.PatientNotes, f => f.Lorem.Sentence())
                .RuleFor(b => b.StaffNotes, f => f.Lorem.Sentence())
                .RuleFor(b => b.Status, f => f.PickRandom<BookingStatus>())
                .RuleFor(b => b.BookingCheckStage, f => f.PickRandom<BookingCheckStage>())
                .RuleFor(b => b.StaffConfirmedAt, f => f.Date.Recent().ToUniversalTime())
                .RuleFor(b => b.ConfirmedByStaffId, f => f.PickRandom(staffMembers).StaffId)
                .RuleFor(b => b.Patient, f => f.PickRandom(patients))
                .RuleFor(b => b.PatientId, (f, b) => b.Patient.PatientId)
                .RuleFor(b => b.Staff, f => f.PickRandom(staffMembers))
                .RuleFor(b => b.StaffId, (f, b) => b.Staff.StaffId)
                .RuleFor(b => b.BookingType, f => f.PickRandom(bookingTypes))
                .RuleFor(b => b.BookingTypeId, (f, b) => b.BookingType.BookingTypeId)
                .RuleFor(b => b.CreatedAt, f => DateTime.UtcNow)
                .RuleFor(b => b.UpdatedAt, f => f.Date.Recent().ToUniversalTime());
        }
    }
}
