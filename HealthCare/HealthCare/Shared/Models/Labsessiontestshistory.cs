using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Labsessiontestshistory
{
    public int Id { get; set; }

    public int LabSessionId { get; set; }

    public int LabTestId { get; set; }

    public string? Result { get; set; }

    public string? Finished { get; set; }

    public decimal? RPrice { get; set; }

    public bool? Sellt { get; set; }

    public string? Schpro { get; set; }

    public string? Schid { get; set; }

    public string? Nno { get; set; }

    public int? Consid { get; set; }

    public int Recno { get; set; }

    public DateTime? Ddate { get; set; }

    public bool? IsCash { get; set; }

    public int LocalId { get; set; }

    public string? Flag { get; set; }

    public string? ResultPercentageValues { get; set; }

    public bool? IsSentToDoc { get; set; }

    public int? ResultsSignedBy { get; set; }

    public int? RangeId { get; set; }

    public DateTime? ResultSentTime { get; set; }

    public byte[] VerCol { get; set; } = null!;

    public int? LabRequestDestinationId { get; set; }

    public int? SchemeOptionTypeId { get; set; }

    public bool? SendToBillingTable { get; set; }

    public string? ResultRange { get; set; }

    public string? ResultComment { get; set; }

    public string? ResultUnit { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
