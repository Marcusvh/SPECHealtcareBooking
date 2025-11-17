using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models.HealthcareStaff
{
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
        public List<string> SupportBookings { get; set; } = new();

    }
}
