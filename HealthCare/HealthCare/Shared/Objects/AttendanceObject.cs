using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Objects
{
    public class AttendanceObject
    {
        public string PatientName { get; set; }
        public string PatientId { get; set;}    
        public DateTime Timestamp { get; set; }
        public int? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public int? ConsultId { get; set; }
        public string? Notes { get; set; }

    }
}
