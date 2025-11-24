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
        private readonly INotifyManager _notify;
        public BookingController(IBookingManager manager, INotifyManager notify)
        {
            _manager = manager;
            _notify = notify;
        }
        // GET: api/<BookingController>
        [HttpGet("bookingType")]
        [ProducesResponseType(typeof(List<BookingType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<List<BookingType>>> GetAllBookingTypes()
        {
            List<BookingType> bookingTypes = await _manager.GetAllBookingTypesAsync();

            if(bookingTypes == null || bookingTypes.Count == 0) return NotFound();

            return Ok(bookingTypes);
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
            ResultResponse<BookingType> result = await _manager.GetBookingTypeByNameAsync(name);
            if (!result.IsSuccess || result.Value == null) return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("booking")]
        [ProducesResponseType(typeof(List<Booking>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingType>> GetAllBookings()
        {
            var booking = await _manager.GetAllBookingsAsync();
            if (booking == null) return NotFound();
            return Ok(booking);
        }
        [HttpGet("booking/id/{id}")]
        [ProducesResponseType(typeof(BookingType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingType>> GetBookingById(Guid id)
        {
            var booking = await _manager.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }
        [HttpPost("booking")]
        [ProducesResponseType(typeof(Booking), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] BookingDTO booking)
        {
            if (booking == null) return BadRequest();

            ResultResponse<Booking> result = await _manager.CreateBookingAsync(booking);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            NotifyStaff notify = new NotifyStaff()
            {
                StaffId = result.Value.StaffId,
                RelatedBookingId = result.Value.BookingId,
                Message = "A new booking has been created.",
                NotificationType = NotificationType.BookingCreated,
                NotificationStatus = NotificationStatus.Sent,
                CreatedAt = DateTime.UtcNow
            };
            await _notify.CreateNotificationForStaffAsync(notify);

            return CreatedAtAction(nameof(GetBookingById), new { id = result.Value.BookingId });
        }
    }
}
