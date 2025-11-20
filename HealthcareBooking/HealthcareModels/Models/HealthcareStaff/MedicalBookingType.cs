using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models.HealthcareStaff
{
    public class MedicalBookingType
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public StaffType RequiredStaffType { get; set; }
        public int DurationMinutes { get; set; }

        public string Department { get; set; }
        public List<string> AllowedSpecialties { get; set; }
    }
}