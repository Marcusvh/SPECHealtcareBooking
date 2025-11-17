using System;
using System.Text.Json.Serialization;

namespace HealthcareModels.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContactMethod
    {
        Email,
        Phone,
        SMS,
        PostalMail
    }

    public class Patient
    {
        public Guid PatientId { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        // Relation to city/clinic/etc
        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        // Prefer: enum instead of free-form string
        public ContactMethod PreferredContactMethod { get; set; }
    }
}
