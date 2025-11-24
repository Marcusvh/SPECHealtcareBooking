using HealthcareBookingAPI.Context;
using HealthcareBookingAPI.DataGeneration;
using HealthcareBookingAPI.DTO;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] // Default response type
    public class DataGenerationController : ControllerBase
    {
        private readonly HealthcareContext _context;
        private readonly PatientDataGeneration _patientDataGeneration;
        private readonly LocationDataGeneration _locationDataGeneration;
        private readonly StaffDataGeneration _staffDataGeneration;
        private readonly BookingDataGeneration _bookingDataGeneration;
        private readonly DataGenerationFacade _dataGenerationFacade;

        public DataGenerationController(HealthcareContext context)
        {
            _context = context;
            _patientDataGeneration = new(context);
            _staffDataGeneration = new(context);
            _bookingDataGeneration = new(context);
            _locationDataGeneration = new();
            _dataGenerationFacade = new(context);
        }

        /// <summary>
        /// Generates a specified number of patients.
        /// </summary>
        /// <param name="amount">Number of patients to generate. Default is 1.</param>
        /// <returns>Message indicating how many patients were generated.</returns>
        [HttpPost("patients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GeneratePatients([FromBody] int amount = 1)
        {
            if (amount <= 0) return BadRequest("Amount must be greater than 0");

            var patients = _patientDataGeneration.GeneratePatient().Generate(amount);
            _context.Patients.AddRange(patients);
            await _context.SaveChangesAsync();

            return Ok($"Generated {amount} patients");
        }

        /// <summary>
        /// Generates a specified number of locations.
        /// </summary>
        [HttpPost("location")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GenerateLocations([FromBody] int amount = 1)
        {
            if (amount <= 0) return BadRequest("Amount must be greater than 0");

            var locations = _locationDataGeneration.GenerateLocation().Generate(amount);
            _context.Locations.AddRange(locations);
            await _context.SaveChangesAsync();

            return Ok($"Generated {amount} locations");
        }

        /// <summary>
        /// Generates a specified number of staff members.
        /// </summary>
        [HttpPost("staff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GenerateStaff([FromBody] int amount = 1)
        {
            if (amount <= 0) return BadRequest("Amount must be greater than 0");

            var staff = _staffDataGeneration.GenerateStaff().Generate(amount);
            _context.Staffs.AddRange(staff);
            await _context.SaveChangesAsync();

            return Ok($"Generated {amount} staff members");
        }

        /// <summary>
        /// Generates a specified number of doctors.
        /// </summary>
        [HttpPost("doctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GenerateDoctor([FromBody] int amount = 1)
        {
            if (amount <= 0) return BadRequest("Amount must be greater than 0");

            var doctors = _staffDataGeneration.GenerateDoctor().Generate(amount);
            _context.Doctors.AddRange(doctors);
            await _context.SaveChangesAsync();

            return Ok($"Generated {amount} doctors");
        }

        /// <summary>
        /// Generates a specified number of nurses.
        /// </summary>
        [HttpPost("nurse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GenerateNurse([FromBody] int amount = 1)
        {
            if (amount <= 0) return BadRequest("Amount must be greater than 0");

            var nurses = _staffDataGeneration.GenerateNurse().Generate(amount);
            _context.Nurses.AddRange(nurses);
            await _context.SaveChangesAsync();

            return Ok($"Generated {amount} nurses");
        }

        /// <summary>
        /// Generates a specified number of medical students.
        /// </summary>
        [HttpPost("medStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GenerateMedStudent([FromBody] int amount = 1)
        {
            if (amount <= 0) return BadRequest("Amount must be greater than 0");

            var doctorIds = _context.Doctors.Select(d => d.StaffId).ToList();
            var medStudents = _staffDataGeneration.GenerateMedStudent(doctorIds).Generate(amount);
            _context.MedicalStudents.AddRange(medStudents);
            await _context.SaveChangesAsync();

            return Ok($"Generated {amount} medical students");
        }

        /// <summary>
        /// Generates a specified number of booking types.
        /// </summary>
        [HttpPost("bookingType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GenerateBookingType([FromBody] int amount = 1)
        {
            if (amount <= 0) return BadRequest("Amount must be greater than 0");

            var bookingTypes = _bookingDataGeneration.GenerateBookingType(amount);
            _context.BookingTypes.AddRange(bookingTypes);
            await _context.SaveChangesAsync();

            return Ok($"Generated {amount} booking types");
        }

        /// <summary>
        /// Generates a specified number of booking records and saves them to the database.
        /// </summary>
        [HttpPost("booking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GenerateBooking([FromBody] int amount = 1)
        {
            if (amount <= 0) return BadRequest("Amount must be greater than 0");

            List<Booking> booking = _bookingDataGeneration.GenerateBooking().Generate(amount);
            _context.Bookings.AddRange(booking);
            await _context.SaveChangesAsync();
            return Ok($"Generated {amount} booking");
        }
        /// <summary>
        /// Generates staff, patients, and locations based on the provided data generation parameters.
        /// </summary>
        /// <remarks>This method delegates the data generation process to the underlying data generation
        /// facade. The response includes a detailed breakdown of the generated entities.</remarks>
        /// <param name="dto">An object containing the parameters for data generation, including the number of locations, patients, and
        /// staff (doctors, nurses, and medical students), as well as an optional fixed location ID.</param>
        /// <returns>An HTTP 200 OK response containing a summary of the generated data, including the counts of locations,
        /// patients, and staff, and whether a fixed location ID was assigned. Returns null if <paramref name="dto"/> is null</returns>
        [HttpPost("staffPatientsLocations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GenerateStaffPatientsLocations([FromBody] DataGenerationStaffPatientsLocationsDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Data generation parameters are required.");
            }
            await _dataGenerationFacade.GenerateStaffPatientsLocations(dto);

            return
                Ok($"Generated:\n" +
                $"- {dto.NumLocation} locations\n" +
                $"- {dto.NumPatient} patients\n" +
                $"- {dto.NumDoctor + dto.NumNurse + dto.NumMedStudent} staff (Doctors: {dto.NumDoctor}, Nurses: {dto.NumNurse}, MedStudents: {dto.NumMedStudent})\n" +
                (dto.FixedLocationId != null
                    ? $"- Fixed location assigned: {dto.FixedLocationId}"
                    : "- No fixed location ID provided"));
        }
        /// <summary>
        /// Generates bookings and booking types based on the specified data generation parameters.
        /// </summary>
        /// <remarks>This method invokes the data generation process through the
        /// <c>_dataGenerationFacade</c> service.  Ensure that the <paramref name="dto"/> parameter contains valid data
        /// to avoid errors.</remarks>
        /// <param name="dto">An object containing the parameters for data generation, including the number of booking types  and bookings
        /// to generate. This parameter cannot be <see langword="null"/>.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation. Returns: <list type="bullet"> <item>
        /// <description><see cref="BadRequestResult"/> if the <paramref name="dto"/> parameter is <see
        /// langword="null"/>.</description> </item> <item> <description><see cref="OkObjectResult"/> with a summary of
        /// the generated bookings and booking types if the operation succeeds.</description> </item> </list></returns>
        [HttpPost("allBookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GenerateBookings([FromBody] DataGenerationBookingsDTO dto)
        {
            if(dto == null)
            {
                return BadRequest("Data generation parameters are required.");
            }
            await _dataGenerationFacade.GenerateBookings(dto);

            return
                Ok("Generated:\n" +
                $"- {dto.NumBookingType} booking types\n " +
                $"- {dto.NumBooking} bookings");
        }
        /// <summary>
        /// Generates test data for the application based on the specified parameters.
        /// </summary>
        /// <remarks>This method generates various types of test data, such as booking types, bookings,
        /// locations, patients, and staff members (doctors, nurses, and medical students). If a fixed location ID is
        /// provided in the input, it will be assigned to the generated data; otherwise, no fixed location will be
        /// used.</remarks>
        /// <param name="dto">An object containing the parameters for data generation, including the number of booking types, bookings,
        /// locations, patients, and staff to generate.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation. Returns <see cref="BadRequestResult"/>
        /// if the input parameters are null, or <see cref="OkObjectResult"/> with a summary of the generated data upon
        /// success.</returns>
        [HttpPost("allData")]
        public async Task<ActionResult> GenerateAllData([FromBody] DataGenerationAllDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Data generation parameters are required");
            }
            await _dataGenerationFacade.GenerateAllData(dto);

            return
                Ok($"Generated:\n" +
                $"- {dto.NumBookingType} booking types\n" +
                $"- {dto.NumBooking} bookings\n" +
                $"- {dto.NumLocation} locations\n" +
                $"- {dto.NumPatient} patients\n" +
                $"- {dto.NumDoctor + dto.NumNurse + dto.NumMedStudent} staff (Doctors: {dto.NumDoctor}, Nurses: {dto.NumNurse}, MedStudents: {dto.NumMedStudent})\n" +
                (dto.FixedLocationId != null
                    ? $"- Fixed location assigned: {dto.FixedLocationId}"
                    : "- No fixed location ID provided"));
        }
    }
}
