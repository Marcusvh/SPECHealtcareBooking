using HealthcareModels.Models.HealthcareStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models
{
    public enum NotificationReason
    {
        AppointmentReminder,
        AppointmentCancellation,
        AppointmentReschedule,
        GeneralUpdate
    }
    public class NotifyPatient
    {
        public Guid PatientNotificationId { get; set; }
        public Guid BookingId { get; set; }
        public Guid PatientId { get; set; }
        public ContactMethod ContactChannelUsed { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public NotificationReason NotificationReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ErrorDetails { get; set; }
        public string? Subject { get; set; }
    }
}
