using Bogus;
using HealthcareModels.Models;

namespace HealthcareBookingAPI.DataGeneration
{
    public class LocationDataGeneration
    {
        public Faker<Location> GenerateLocation()
        {
            // Set locale to Danish
            var faker = new Faker("nb_NO");

            return new Faker<Location>()
                .RuleFor(l => l.LocationId, f => Guid.NewGuid())
                .RuleFor(l => l.City, f => faker.Address.City())
                .RuleFor(l => l.Region, f => faker.Address.State())
                .RuleFor(l => l.PostalCode, f => faker.Address.ZipCode())
                .RuleFor(l => l.Country, f => "Norway"); // hardcode country
        }
    }
}
