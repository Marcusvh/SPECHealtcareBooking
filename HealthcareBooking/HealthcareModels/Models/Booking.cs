using HealthcareModels.Models.HealthcareStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public DateTime Date { get; set; }
        public string PatientName { get; set; }

        
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; }
    }
}
