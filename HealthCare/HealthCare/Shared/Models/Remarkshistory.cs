using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Remarkshistory
{
    public int PatientId { get; set; }

    public string? PatientNo { get; set; }

    public DateTime? TransDate { get; set; }

    public int ConsId { get; set; }

    public string? Remark { get; set; }

    public int? SentTo { get; set; }

    public bool? IsWard { get; set; }

    public string? Comments { get; set; }

    public string? CommentsTwo { get; set; }

    public int UserId { get; set; }

    public DateTime? AdmittedOrReferredDate { get; set; }

    public DateTime? TransTime { get; set; }

    public int Id { get; set; }
}
