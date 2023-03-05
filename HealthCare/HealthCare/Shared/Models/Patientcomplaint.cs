using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Patientcomplaint
{
    public int PatientId { get; set; }

    public int ConsultId { get; set; }

    public int ComplaintId { get; set; }

    public DateTime PatientComplaintDate { get; set; }
}
