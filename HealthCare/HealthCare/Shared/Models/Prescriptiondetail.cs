using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Prescriptiondetail
{
    public int RecNo { get; set; }

    public string? PrescriptionSessionId { get; set; }

    public int? ConsId { get; set; }

    public string? DrugId { get; set; }

    public int? FrequencyQty { get; set; }

    public string? Frequency { get; set; }

    public int? NoofDays { get; set; }

    public decimal? Total { get; set; }

    public decimal? DispTotal { get; set; }

    public string? Notes { get; set; }

    public bool? IsDispensed { get; set; }

    public DateTime? Ddate { get; set; }

    public bool? IsPrescriptionIssued { get; set; }

    public int? EditedBy { get; set; }

    public DateTime? EditedDate { get; set; }
}
