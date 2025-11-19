using HealthcareModels.Models.HealthcareStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models
{
    public class BookingType
    {
        public Guid BookingTypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        // Staff that can perform this/these booking type
        public List<Staff> StaffMembers { get; set; } = new();

        // Bookings of this type
        public List<Booking> Bookings { get; set; } = new();
    }

}
