using HealthCare.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Objects
{
    public class DoctorAnalysis
    {
        public List<TopPrescribedDrug> TopPrescribedDrugs { get; set; }
        public List<AttendanceObject> AttendanceObjects { get; set; }
        public List<Cases> TotalCases { get; set; }
        public List<Cases> TopCases { get; set; }
    }
}
