using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Labprice
{
    public string Latestid { get; set; } = null!;

    public string Schid { get; set; } = null!;

    public decimal? Priceid { get; set; }

    public bool? IsActive { get; set; }

    public int LabRequestDestinationId { get; set; }
}
