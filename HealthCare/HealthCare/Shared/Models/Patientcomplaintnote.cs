using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Patientcomplaintnote
{
    public int PatientId { get; set; }

    public int ConsultId { get; set; }

    public string Notes { get; set; } = null!;

    public DateTime PatientComplaintNotesDate { get; set; }

    public int UserId { get; set; }
}
