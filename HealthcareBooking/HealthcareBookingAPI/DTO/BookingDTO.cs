using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using System.Text.Json.Serialization;

namespace HealthcareBookingAPI.DTO
{
    public class BookingDTO
    {
        public DateTime StartTime { get; set; }
        public string? PatientNotes { get; set; }
        public Guid BookingTypeId { get; set; }
        public Guid PatientId { get; set; }
        public Guid? StaffId { get; set; }

    }
}
