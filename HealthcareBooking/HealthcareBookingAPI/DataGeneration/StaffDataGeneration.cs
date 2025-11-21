using Bogus;
using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.Helpers;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HealthcareBookingAPI.DataGeneration
{
    public class StaffDataGeneration
    {
        private readonly HealthcareContext _context;
        private readonly List<BookingType> _bookingTypes;
        private readonly List<MedicalBookingType> _medicalBookingTypes;
        private readonly string[] NurseCertifications =
        {
            "BLS", "ACLS", "PALS", "TNCC", "ENPC", "CPN", "CCRN"
        };

        public readonly Dictionary<string, string> SpecialtyDepartmentMap = new()
        {
            ["General Medicine"] = "General Medicine",
            ["Family Medicine"] = "General Medicine",
            ["Cardiology"] = "Cardiology",
            ["Dermatology"] = "Dermatology",
            ["Neurology"] = "Neurology",
            ["Pediatrics"] = "Pediatrics",
            ["Orthopedics"] = "Orthopedics",
            ["Psychiatry"] = "Psychiatry",
            ["Oncology"] = "Oncology"
        };


        public StaffDataGeneration(HealthcareContext context)
        {
            _context = context;
            _bookingTypes = context.BookingTypes.ToList();
            _medicalBookingTypes = GenerateStaticMedicalBookingTypeData.MedicalBookingTypes;
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
            var specList = SpecialtyDepartmentMap.Keys.ToList();

            return new Faker<Doctor>()
                .RuleFor(d => d.StaffId, f => Guid.NewGuid())
                .RuleFor(d => d.Name, f => $"Dr. {f.Name.FullName()}")
                .RuleFor(d => d.Description, f => f.Lorem.Sentence())
                .RuleFor(d => d.Type, f => StaffType.Doctor)
                .RuleFor(d => d.Specialties, f => f.PickRandom(specList))
                .RuleFor(d => d.AssignedDepartment, (f, d) => SpecialtyDepartmentMap[d.Specialties])
                .RuleFor(d => d.MedicalLincenseNumber, f => $"LIC-{f.Random.Number(100000, 999999)}")
                .RuleFor(d => d.YearsOfExperience, f => f.Random.Int(1, 35))
                .RuleFor(d => d.IsAcceptingNewPatients, f => f.Random.Bool())
                .RuleFor(d => d.SupportedBookingTypeIds, (f, d) =>
                {
                    var validNames = _medicalBookingTypes
                        .Where(x => x.RequiredStaffType == StaffType.Doctor &&
                                    x.AllowedSpecialties.Contains(d.Specialties))
                        .Select(x => x.Name)
                        .ToList();

                    return _context.BookingTypes
                        .Where(bt => validNames.Contains(bt.Name))
                        .Select(bt => bt.BookingTypeId)
                        .ToList();
                });


        }

        // -----------------------------
        // Nurse faker
        // -----------------------------
        public Faker<Nurse> GenerateNurse()
        {
            return new Faker<Nurse>()
                .RuleFor(n => n.StaffId, f => Guid.NewGuid())
                .RuleFor(n => n.Name, f => f.Name.FullName())
                .RuleFor(n => n.Description, f => f.Lorem.Sentence())
                .RuleFor(n => n.Type, f => StaffType.Nurse)
                .RuleFor(n => n.NursingLevel, f => f.PickRandom("RN", "LPN", "NP", "CNS"))
                .RuleFor(n => n.AssignedDepartment, f => f.PickRandom(SpecialtyDepartmentMap.Values.ToList()))
                .RuleFor(n => n.Certification, f => f.PickRandom(NurseCertifications, f.Random.Int(1, 4)).ToList())
                .RuleFor(n => n.YearsOfExperience, f => f.Random.Int(0, 25))
                .RuleFor(n => n.SupportedBookingTypeIds, f =>
                {
                    // Step 1: Find the *valid definition types* from your in-memory list
                    var validNames = _medicalBookingTypes
                        .Where(x =>
                            x.RequiredStaffType == StaffType.Nurse)
                        .Select(x => x.Name)
                        .ToList();

                    // Step 2: Pull the matching BookingTypes from the DB
                    return _context.BookingTypes
                        .Where(bt => validNames.Contains(bt.Name))
                        .Select(bt => bt.BookingTypeId)
                        .ToList();

                });
        }
        // -----------------------------
        // MedicalStudent faker
        // -----------------------------
        public Faker<MedicalStudent> GenerateMedStudent(List<Guid> doctorIds)
        {
            return new Faker<MedicalStudent>()
                .RuleFor(ms => ms.StaffId, f => Guid.NewGuid())
                .RuleFor(ms => ms.Name, f => f.Name.FullName())
                .RuleFor(ms => ms.Type, f => StaffType.MedicalStudent)
                .RuleFor(ms => ms.Description, f => f.Lorem.Sentence())
                .RuleFor(ms => ms.University, f => $"{f.Company.CompanyName()} Medical School")
                .RuleFor(ms => ms.SupervisorId, f => f.PickRandom(doctorIds))
                .RuleFor(ms => ms.YearOfStudy, f => f.Random.Int(1, 6))
                .RuleFor(ms => ms.InternshipStartDate, f => DateOnly.FromDateTime(f.Date.Past(1)))
                .RuleFor(ms => ms.InternshipEndDate,
                    (f, ms) => ms.InternshipStartDate.AddMonths(f.Random.Int(3, 12)))
                .RuleFor(ms => ms.SupportedBookingTypeIds, f =>
                {
                    var validNames = _medicalBookingTypes
                        .Where(x =>
                            x.RequiredStaffType == StaffType.Nurse || // med students assist nurses
                            x.RequiredStaffType == StaffType.Doctor &&
                            x.DurationMinutes <= 20)                  // can assist in short consults
                        .Select(x => x.Name)
                        .ToList();

                    // Step 2: Pull the matching BookingTypes from the DB
                    return _context.BookingTypes
                        .Where(bt => validNames.Contains(bt.Name))
                        .Select(bt => bt.BookingTypeId)
                        .ToList();
                });
        }

    }
}
