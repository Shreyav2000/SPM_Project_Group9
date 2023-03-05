using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models; 

public partial class Prescription
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string? PatientNo { get; set; }

    public int? ConsId { get; set; }

    public string? PrescriptionSessionId { get; set; }

    public int? DoctorRequesting { get; set; }

    public string? DoctorDetails { get; set; }

    public DateTime? TransDate { get; set; }

    public DateOnly? DrugsRefillDate { get; set; }

    public bool? SendSms { get; set; }

    public int? NotifyViaSmsaheadDays { get; set; }

    public bool? SendEmail { get; set; }

    public int? NotifyViaEmailaheadDays { get; set; }

    public bool? IsActive { get; set; }
}
