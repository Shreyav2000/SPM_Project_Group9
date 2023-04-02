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
        public int Id { get; set; } 
        public Patient Patient { get; set; }
        public string ComplaintNotes { get; set; }
        public List<Patientcomplaint> Complaints { get; set; }
        public List<Prescriptiondetail> Prescriptions { get; set; }
    }
}
