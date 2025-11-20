using HealthcareBookingAPI.DTO;
using HealthcareBookingAPI.Interfaces;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager _manager;
        public BookingController(IBookingManager manager)
        {
            _manager = manager;
        }
        // GET: api/<BookingController>
        [HttpGet("bookingType")]
        public async Task<ActionResult<List<BookingType>>> GetAllBookingTypes()
        {
            return Ok(await _manager.GetAllBookingTypesAsync());
        }

        // GET api/staff/bookingType/id/{id}
        [HttpGet("bookingType/id/{id}")]
        public async Task<ActionResult<BookingType>> GetBookingTypeById(Guid id)
        {
            var bookingType = await _manager.GetBookingTypeByIdAsync(id);
            if (bookingType == null) return NotFound();
            return Ok(bookingType);
        }

        // GET api/staff/bookingType/name/{name}
        [HttpGet("bookingType/name/{name}")]
        public async Task<ActionResult<BookingType>> GetBookingTypeByName(string name)
        {
            var bookingType = await _manager.GetBookingTypeByNameAsync(name);
            if (bookingType == null) return NotFound();
            return Ok(bookingType);
        }

        [HttpGet("booking")]
        public async Task<ActionResult<BookingType>> GetAllBookings()
        {
            var booking = await _manager.GetAllBookingsAsync();
            if (booking == null) return NotFound();
            return Ok(booking);
        }
        [HttpGet("booking/id/{id}")]
        public async Task<ActionResult<BookingType>> GetBookingById(Guid id)
        {
            var booking = await _manager.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }
        [HttpPost("booking")]
        public async Task<ActionResult<string>> CreateBooking([FromBody] BookingDTO booking)
        {
            if (booking == null) return BadRequest();
            await _manager.CreateBooking(booking);
            return Ok(booking);
        }
    }
}
