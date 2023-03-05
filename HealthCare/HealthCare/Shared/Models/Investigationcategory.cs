using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Investigationcategory
{
    public int InvestigationCategoryId { get; set; }

    public string InvestigationCategory1 { get; set; } = null!;

    public virtual ICollection<Labtest> Labtests { get; } = new List<Labtest>();
}
