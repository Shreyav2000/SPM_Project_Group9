using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Patientattendance
{
    public int PatientId { get; set; }

    public string PatientNo { get; set; } = null!;

    public DateOnly PatListdate { get; set; }

    public string? Reason { get; set; }

    public DateTime PTime { get; set; }

    public bool SeenDoctor { get; set; }

    public int ConsultId { get; set; }

    public string? BillState { get; set; }

    public int? Provider { get; set; }

    public string? PatientCategory { get; set; }

    public int? SeenByDoctorId { get; set; }

    public string? AgePrefix { get; set; }

    public int? PatientCategorySubTypeId { get; set; }

    public int? PatientDepartmentId { get; set; }

    public DateTime? SeenByDoctorTime { get; set; }

    public DateTime? FinalDischargeDate { get; set; }

    public string? DepartmentCode { get; set; }
}
