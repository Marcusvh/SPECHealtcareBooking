using Bogus;
using HealthcareModels.Models;

namespace HealthcareBookingAPI.DataGeneration
{
    public class LocationDataGeneration
    {
        public Faker<Location> GenerateLocation()
        {
            return new Faker<Location>("nb_NO") 
                .RuleFor(l => l.LocationId, f => Guid.NewGuid())
                .RuleFor(l => l.City, f => f.Address.City())
                .RuleFor(l => l.Region, f => f.Address.State())
                .RuleFor(l => l.PostalCode, f => f.Address.ZipCode())
                .RuleFor(l => l.Country, f => "Norway");
        }
    }

}
