using HealthCare.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Objects
{
    public class SessionObject
    {
        public Patient Patient { get; set; }
        public string Complaint { get; set; }
        public List<Prescriptiondetail> Prescriptions { get; set; }
    }
}
