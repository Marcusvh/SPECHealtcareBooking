using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthcareModels.Models.HealthcareStaff
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NotificationStatus {
        Sent, 
        Read, 
        Failed
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NotificationType
    {
        BookingCreated,
        BookingUpdated,
        BookingCancelled,
        General
    }
    public class NotifyStaff
    {
        public Guid NotifyStaffId { get; set; }
        public string? Message { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public Guid RelatedBookingId { get; set; }
        public Booking RelatedBooking { get; set; }
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; }
    }
}
