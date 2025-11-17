using HealthcareModels.Models.HealthcareStaff;
using System;
using System.Text.Json.Serialization;

namespace HealthcareModels.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BookingStatus
    {
        Scheduled,
        Completed,
        Cancelled,
        NoShow
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CheckStage
    {
        First,
        Second,
        Cancelled,
        Confirmed
    }
    public class Booking
    {
        public Guid BookingId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string BookingType { get; set; }
        public string? PatientNotes { get; set; }
        public string? StaffNotes { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Scheduled;
        public CheckStage CheckStage { get; set; } = CheckStage.First;
        public Guid StaffConfirmedAt { get; set; }
        public DateTime ConfirmedByStaffId { get; set; }

        // References
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
