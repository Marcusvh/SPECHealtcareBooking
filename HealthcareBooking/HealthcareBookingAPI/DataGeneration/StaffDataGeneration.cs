using Bogus;
using HealthcareBookingAPI.Context;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using System.Linq;

namespace HealthcareBookingAPI.DataGeneration
{
    public class StaffDataGeneration
    {
        private readonly HealthcareContext _context;
        private readonly List<BookingType> _bookingTypes;

        private readonly string[] DoctorSpecialties =
        {
            "Cardiology", "Dermatology", "Neurology", "General Medicine",
            "Pediatrics", "Orthopedics", "Psychiatry", "Oncology"
        };

        private readonly string[] NurseCertifications =
        {
            "BLS", "ACLS", "PALS", "TNCC", "ENPC", "CPN", "CCRN"
        };

        private readonly string[] Departments =
        {
            "Emergency", "ICU", "Pediatrics", "Surgery", "Dermatology",
            "General Medicine", "Orthopedics"
        };

        public StaffDataGeneration(HealthcareContext context)
        {
            _context = context;
            _bookingTypes = _context.bookingTypes.ToList();
        }

        private List<BookingType> PickRandomBookingTypes(int amount)
        {
            Random random = new Random();

            // Use IDs to avoid equality issues with EF entities
            HashSet<Guid> selectedIds = new HashSet<Guid>();

            while (selectedIds.Count < amount)
            {
                var randomType = _bookingTypes[random.Next(_bookingTypes.Count)];
                selectedIds.Add(randomType.BookingTypeId);
            }

            return _bookingTypes
                .Where(b => selectedIds.Contains(b.BookingTypeId))
                .ToList();
        }

        public Faker<Staff> GenerateStaff()
        {
            return new Faker<Staff>("nb_NO")
                .RuleFor(s => s.StaffId, f => Guid.NewGuid())
                .RuleFor(s => s.Name, f => f.Name.FullName())
                .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                .RuleFor(s => s.Type, f => f.PickRandom<StaffType>())
                .RuleFor(s => s.SupportedBookingTypes,
                    f => PickRandomBookingTypes(f.Random.Int(1, 4)));
        }
        // -----------------------------
        // Doctor faker
        // -----------------------------
        public Faker<Doctor> GenerateDoctor()
        {
            return new Faker<Doctor>("nb_NO")
                .RuleFor(d => d.StaffId, f => Guid.NewGuid())
                .RuleFor(d => d.Name, f => $"Dr. {f.Name.FullName()}")
                .RuleFor(d => d.Description, f => f.Lorem.Paragraph())
                .RuleFor(d => d.Type, f => StaffType.Doctor)
                .RuleFor(d => d.Specialties, f => f.PickRandom(DoctorSpecialties))
                .RuleFor(d => d.MedicalLincenseNumber, f => $"LIC-{f.Random.Number(100000, 999999)}")
                .RuleFor(d => d.YearsOfExperience, f => f.Random.Int(1, 40))
                .RuleFor(d => d.IsAcceptingNewPatients, f => f.Random.Bool())
                .RuleFor(d => d.AssignedDepartment, f => f.PickRandom(Departments))
                .RuleFor(d => d.SupportedBookingTypes,
                    f => PickRandomBookingTypes(f.Random.Int(1, 4)));
        }
        // -----------------------------
        // Nurse faker
        // -----------------------------
        public Faker<Nurse> GenerateNurse()
        {
            return new Faker<Nurse>("nb_NO")
                .RuleFor(n => n.StaffId, f => Guid.NewGuid())
                .RuleFor(n => n.Name, f => f.Name.FullName())
                .RuleFor(n => n.Description, f => f.Lorem.Sentence())
                .RuleFor(n => n.Type, f => StaffType.Nurse)
                .RuleFor(n => n.NursingLevel, f => f.PickRandom("RN", "LPN", "NP", "CNS"))
                .RuleFor(n => n.Certification, f => f.PickRandom(NurseCertifications, f.Random.Int(1, 4)).ToList())
                .RuleFor(n => n.AssignedDepartment, f => f.PickRandom(Departments))
                .RuleFor(n => n.ShiftType, f => f.PickRandom<ShiftType>())
                .RuleFor(n => n.YearsOfExperience, f => f.Random.Int(0, 30))
                .RuleFor(n => n.SupportedBookingTypes,
                    f => PickRandomBookingTypes(f.Random.Int(1, 4)));
        }

        // -----------------------------
        // MedicalStudent faker
        // -----------------------------
        public Faker<MedicalStudent> GenerateMedStudent()
        {
            List<Guid> doctorID = new List<Guid>();
            doctorID = _context.Doctors.Select(o => o.StaffId).ToList();

            return new Faker<MedicalStudent>("nb_NO")
                .RuleFor(ms => ms.StaffId, f => Guid.NewGuid())
                .RuleFor(ms => ms.Name, f => f.Name.FullName())
                .RuleFor(ms => ms.Description, f => f.Lorem.Sentence())
                .RuleFor(ms => ms.Type, f => StaffType.MedicalStudent)
                .RuleFor(ms => ms.University, f => f.Company.CompanyName() + " Medical School")
                .RuleFor(ms => ms.YearOfStudy, f => f.Random.Int(1, 6))
                .RuleFor(ms => ms.SupervisorId, f => f.PickRandom(doctorID))
                .RuleFor(ms => ms.InternshipStartDate, f => f.Date.Past(1).ToUniversalTime())
                .RuleFor(ms => ms.InternshipEndDate, (f, ms) => ms.InternshipStartDate.AddMonths(f.Random.Int(3, 12)))
                .RuleFor(ms => ms.SupportedBookingTypes,
                    f => PickRandomBookingTypes(f.Random.Int(1, 4)));
        }
    }
}
