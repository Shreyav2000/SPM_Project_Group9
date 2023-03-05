using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Expiryitem
{
    public string? ItemId { get; set; }

    public string? Quantity { get; set; }

    public string? Shelf { get; set; }

    public DateTime? ManDate { get; set; }

    public DateTime? ExpDate { get; set; }

    public string Nid { get; set; } = null!;
}
