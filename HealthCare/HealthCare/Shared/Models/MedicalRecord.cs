using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public int NurseId { get; set; }

    public DateTime RecordDate { get; set; }

    public string Diagnosis { get; set; } = null!;

    public string Treatment { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Nurse Nurse { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; } = new List<Prescription>();
}
