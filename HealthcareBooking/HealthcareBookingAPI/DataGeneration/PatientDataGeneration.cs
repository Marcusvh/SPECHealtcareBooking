using Bogus;
using HealthcareBookingAPI.Context;
using HealthcareModels.Models;

namespace HealthcareBookingAPI.DataGeneration
{
    public class PatientDataGeneration
    {
        private readonly HealthcareContext _context;
        public PatientDataGeneration(HealthcareContext context) 
        {
            _context = context;
        }
        public Faker<Patient> GeneratePatient(Guid? fixedLocationId = null)
        {
            var faker = new Faker<Patient>("nb_NO");
            List<Location> locations = _context.Locations.ToList();                

            return faker
                .RuleFor(p => p.PatientId, f => Guid.NewGuid())
                .RuleFor(p => p.FullName, f => f.Name.FullName())
                .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.FullName))
                .RuleFor(p => p.Phone, f => f.Phone.PhoneNumber("+47 ## ## ## ##"))
                .RuleFor(p => p.DateOfBirth, f => f.Date.Past(80, DateTime.Today.AddYears(-18)).ToUniversalTime())
                .RuleFor(p => p.Address, f => f.Address.StreetAddress())
                .RuleFor(p => p.PreferredContactMethod, f => f.PickRandom<ContactMethod>())
                .RuleFor(p => p.Location, f =>
                    fixedLocationId != null
                        ? locations.First(l => l.LocationId == fixedLocationId)
                        : f.PickRandom(locations)
                )
                .RuleFor(p => p.LocationId, (f, p) => p.Location.LocationId);
        }
    }
}
