using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.Interfaces;
using HealthcareBookingAPI.Managers;
using HealthcareModels.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatient patientManager;
        public PatientController(IPatient patient)
        {
            patientManager = patient;
        }
        // GET: api/<PatientController>
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetAllPatients()
        {
            return await patientManager.GetAllPatients();
        }

        
    }
}
