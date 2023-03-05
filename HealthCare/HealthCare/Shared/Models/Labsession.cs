using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Labsession
{
    public int LabSessionId { get; set; }

    public int PatientId { get; set; }

    public string? PatientNo { get; set; }

    public int? StaffId { get; set; }

    public string? Finished { get; set; }

    public DateTime? LabSessionDate { get; set; }

    public string? Checked { get; set; }

    public DateTime? SampDate { get; set; }

    public string? Doctor { get; set; }

    public int? CategoryId { get; set; }

    public string? Comments { get; set; }

    public DateTime? DateReported { get; set; }

    public string? Schemeid { get; set; }

    public int? SchPro { get; set; }

    public string? Nhilno { get; set; }

    public int? Consid { get; set; }

    public int? LabNo { get; set; }

    public int PaymentStatus { get; set; }

    public string? BillId { get; set; }

    public string? UniqueTransactionId { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? BilledAmount { get; set; }

    public string? StandardLabNo { get; set; }

    public byte[]? LabNoBarcode { get; set; }

    public string? LabBarcodeStandardNo { get; set; }

    public int? RequestingFacilityId { get; set; }

    public int? ServicePointDescriptionId { get; set; }

    public string? ContactAddress { get; set; }

    public bool? IsVoid { get; set; }

    public byte[] VerCol { get; set; } = null!;

    public bool? BroughtInLab { get; set; }
}
