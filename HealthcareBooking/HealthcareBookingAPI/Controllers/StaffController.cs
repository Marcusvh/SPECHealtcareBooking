using HealthcareBookingAPI.Interfaces;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IDoctorManager _manager;
        public StaffController(IDoctorManager manager)
        {
            _manager = manager;
        }
        // GET: api/<StaffController>
        [HttpGet("doctor")]
        public async Task<ActionResult<List<Doctor>>> GetAllDoctors([FromQuery] int? numDoctors)
        {
            return Ok(await _manager.GetAllDoctorsAsync(numDoctors));
        }

        // GET api/staff/doctor/id/{id}
        [HttpGet("doctor/id/{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorById(Guid id)
        {
            var doctor = await _manager.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        // GET api/staff/doctor/name/{name}
        [HttpGet("doctor/name/{name}")]
        public async Task<ActionResult<Doctor>> GetDoctorByName(string name)
        {
            var doctor = await _manager.GetDoctorByNameAsync(name);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }


       
    }
}
