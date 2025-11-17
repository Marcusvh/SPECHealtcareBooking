using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models
{
    public class Patient
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PreferedContactMethod {  get; set; } // support multiple
    }
}
