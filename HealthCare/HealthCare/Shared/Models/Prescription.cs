using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int DrugId { get; set; }

    public int RecordId { get; set; }

    public int Quantity { get; set; }

    public string? Instructions { get; set; }

    public virtual Drug Drug { get; set; } = null!;

    public virtual MedicalRecord Record { get; set; } = null!;
}
