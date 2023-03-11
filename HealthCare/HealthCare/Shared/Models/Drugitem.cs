using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Drugitem
{
    public string ItemId { get; private set; } = string.Empty;

    public string? DrugCodes { get; set; }

    public int? StrengthUnitId { get; set; }

    public decimal? QtyPerBasicPackageForm { get; set; }

    public decimal? Price { get; set; }

    public decimal? UsageFormValue { get; set; }
    public bool OutOfService { get; set; }
    public bool Refill { get; set; }
    public void SetItemId(string a_drugId)
    {
        ItemId= a_drugId;
    }
}
