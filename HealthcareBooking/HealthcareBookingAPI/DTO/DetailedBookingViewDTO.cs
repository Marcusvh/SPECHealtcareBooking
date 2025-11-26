using HealthcareModels.Models;

namespace HealthcareBookingAPI.DTO
{
    public class DetailedBookingViewDTO
    {
        public Guid DetailedBookingViewid { get; set; }

        public string PatientFullName { get; set; }
        public DateOnly DOB { get; set; }
        public string? PatientNotes { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientEmail { get; set; }

        public BookingStatus BookingStatus { get; set; }
        public BookingCheckStage BookingStage { get; set; }
        public string? StaffNotes { get; set; }
        public DateTime StartTime { get; set; }
        public string BookingTypeName { get; set; }
        public TimeOnly Duration { get; set; }

    }
}
