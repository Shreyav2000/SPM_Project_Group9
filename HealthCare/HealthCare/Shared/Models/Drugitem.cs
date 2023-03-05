using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Drugitem
{
    public long ItemId { get; set; }

    public string? DrugCodes { get; set; }

    public int? StrengthUnitId { get; set; }

    public decimal? QtyPerBasicPackageForm { get; set; }

    public string? DrugLevel { get; set; }

    public decimal? Price { get; set; }

    public decimal? UsageFormValue { get; set; }
}
