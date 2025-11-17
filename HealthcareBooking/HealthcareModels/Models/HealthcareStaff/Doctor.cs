using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models.HealthcareStaff
{
    public class Doctor : Staff
    {
        public string Specialties { get; set; }
        public string MedicalLincenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsAcceptingNewPatients { get; set; }
        public string AssignedDepartment { get; set; }
    }
}
