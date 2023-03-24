using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Objects;

public partial class Cases
{
    public int Count { get; set; } = 0;
    public string Day { get;set; } = string.Empty;
    public string Case { get; set; } = null!;
}
