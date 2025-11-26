using HealthcareBookingAPI.Interfaces;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.ComponentModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private readonly INotifyManager _manager;
        public NotifyController(INotifyManager manager)
        {
            _manager = manager;
        }
        // GET: api/<NotifyController>
        [HttpGet("staff")]
        public async Task<ActionResult<List<NotifyStaff>>> GetAllNotifications()
        {
            return Ok(await _manager.GetAllNotificationsAsync());
        }

        // GET api/<NotifyController>/5
        [HttpGet("staff/id/{id}")]
        public async Task<ActionResult<List<NotifyStaff>>> GetAllNotificationsByStaffId(Guid id)
        {
            return Ok(await _manager.GetAllNotificationsByStaffIdAsync(id));
        }
        [HttpPatch("staff/id/{id}/read")]
        [ProducesResponseType(typeof(NotifyStaff), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyStaff>> UpdateNotificationStatus(Guid id)
        {
            var result = await _manager.UpdateNotificationStatusAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

    }
}
