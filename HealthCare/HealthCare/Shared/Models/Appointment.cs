using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public int NurseId { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Nurse Nurse { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
