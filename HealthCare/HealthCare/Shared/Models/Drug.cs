using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Drug
{
    public string DrugId { get; set; } = null!;

    public string? Drugname { get; set; }
}
