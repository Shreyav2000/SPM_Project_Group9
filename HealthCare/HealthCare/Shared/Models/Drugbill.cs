using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Drugbill
{
    public int BranchId { get; set; }

    public long ReceiptId { get; set; }

    public string ReceiptNo { get; set; } = null!;

    public int CustomerId { get; set; }

    public string CustomerNo { get; set; } = null!;

    public long BillId { get; set; }

    public DateTime AsOfDate { get; set; }

    public decimal? Amount { get; set; }

    public string? CustomerName { get; set; }

    public int ConsultId { get; set; }

    public int PatientId { get; set; }

    public string? HbillId { get; set; }

    public string? UniqueTransactionId { get; set; }
}
