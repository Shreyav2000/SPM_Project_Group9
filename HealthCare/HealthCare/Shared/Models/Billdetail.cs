using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Billdetail
{
    public string BillId { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public virtual Bill Bill { get; set; } = null!;
}
