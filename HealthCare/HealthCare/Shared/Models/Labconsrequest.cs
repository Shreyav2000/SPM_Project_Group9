using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Labconsrequest
{
    public int PatientId { get; set; }

    public string? PatientNo { get; set; }

    public int LabtestId { get; set; }

    public int ConsId { get; set; }

    public DateTime? TransDate { get; set; }

    public bool? IsAccessed { get; set; }

    public int? UserId { get; set; }

    public int RecId { get; set; }

    public DateTime? RequestSentByDoctorTime { get; set; }

    public int? InvestigationCategoryId { get; set; }
}
