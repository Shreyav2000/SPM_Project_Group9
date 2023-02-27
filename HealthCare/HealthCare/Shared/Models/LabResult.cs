using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class LabResult
{
    public int ResultId { get; set; }

    public int TestId { get; set; }

    public int TechnicianId { get; set; }

    public int PatientId { get; set; }

    public DateTime ResultDate { get; set; }

    public string ResultText { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual LabTechnician Technician { get; set; } = null!;

    public virtual LabTest Test { get; set; } = null!;
}
