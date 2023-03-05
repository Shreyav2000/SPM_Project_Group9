using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Drugbrand
{
    public string BrandId { get; set; } = null!;

    public string BrandName { get; set; } = null!;

    public string? ItemId { get; set; }
}
