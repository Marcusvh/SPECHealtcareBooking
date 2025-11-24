using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthcareModels.Models.HealthcareStaff
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StaffType
    {
        Doctor,
        Nurse,
        MedicalStudent,
        Other
    }
    public class Staff
    {
        public Guid StaffId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StaffType Type { get; set; } // doctor, nurse, medicine students
        public List<Guid> SupportedBookingTypeIds { get; set; }
        public List<BookingType> SupportedBookingTypes { get; set; } = new();
        public List<NotifyStaff> NotifyStaffs { get; set; } = new();
    }
}
