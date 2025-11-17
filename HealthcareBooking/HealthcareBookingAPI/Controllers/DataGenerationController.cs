using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.DataGeneration;
using HealthcareModels.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataGenerationController : ControllerBase
    {
        private readonly HealthcareContext _context;
        private readonly PatientDataGeneration patientDataGeneration;
        public DataGenerationController(HealthcareContext context)
        {
            patientDataGeneration = new(context);
            _context = context;
        }
        // GET: api/<DataGenerationController>
        [HttpPost("patients")]
        public async Task<string> GeneratePatients([FromBody] int amount = 1)
        {
            List<Patient> patients = patientDataGeneration.Create().Generate(amount);
            _context.Patients.AddRange(patients);
            await _context.SaveChangesAsync();
            return $"Generated {amount} patients";
        }

        // GET api/<DataGenerationController>/5
        [HttpPost("location")]
        public async Task<string> GenerateLocations([FromBody] int amount = 1)
        {
            List<Location> locations = LocationDataGeneration.Create().Generate(amount);
            _context.Locations.AddRange(locations);
            await _context.SaveChangesAsync();
            return $"Generated {amount} locations";
        }

        // POST api/<DataGenerationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DataGenerationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DataGenerationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
