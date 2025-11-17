using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models.HealthcareStaff
{
    public enum ShiftType { Day, Night, Rotating }
    public class Nurse : Staff
    {
        public string NursingLevel { get; set; }
        public List<string> Certification {  get; set; }
        public string AssignedDepartment { get; set; }
        public ShiftType ShiftType { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
