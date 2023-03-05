using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Itemquantitysummary
{
    public int TransId { get; set; }

    public int? ItemId { get; set; }

    public string? BrandId { get; set; }

    public int? BranchId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? Lastupdateddate { get; set; }
}
