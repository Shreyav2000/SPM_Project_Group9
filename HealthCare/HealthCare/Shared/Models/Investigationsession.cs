using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Investigationsession
{
    public int InvestigationSessionId { get; set; }

    public int? PatientId { get; set; }

    public string? PatientNo { get; set; }

    public DateTime? SessionDate { get; set; }

    public string? Comments { get; set; }

    public int? RequestedBy { get; set; }

    public int? ConsId { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? BillAmount { get; set; }

    public int PaymentStatus { get; set; }

    public string? BillId { get; set; }

    public string? UniqueTransactionId { get; set; }

    public int? InvestigationCategoryId { get; set; }

    public DateTime? SessionTime { get; set; }

    public bool? IsVoid { get; set; }

    public int? IsVoidBy { get; set; }

    public DateTime? IsVoidDate { get; set; }
}
