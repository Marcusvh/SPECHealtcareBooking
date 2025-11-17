using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models.HealthcareStaff
{
    public class MedicalStudent : Staff
    {
        public string University { get; set; }
        public int YearOfStudy { get; set; }
        public Guid SupervisorId { get; set; }
        public DateTime InternshipStartDate { get; set; }
        public DateTime InternshipEndDate { get; set; }
    }
}
