using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class SoldDrug
{
    public int SaleId { get; set; }

    public int DrugId { get; set; }

    public int PatientId { get; set; }

    public DateTime SaleDate { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Drug Drug { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
