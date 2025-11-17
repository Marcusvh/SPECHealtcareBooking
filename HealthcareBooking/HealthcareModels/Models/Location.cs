using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareModels.Models
{
    public class Location
    {
        public Guid LocationId { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string? FullAddress => $"{City}, {Region} {PostalCode}, {Country}";

    }
}
