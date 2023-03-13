using HealthCare.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Objects
{
    public class MedHistory
    {
        public string DoctorName { get; set; }
        public string Complaint { get; set; }
        public DateTime Timestamp { get; set; }
        public int ConsultId { get; set; }
    }
}
