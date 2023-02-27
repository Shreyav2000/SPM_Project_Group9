using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class LabTest
{
    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<LabResult> LabResults { get; } = new List<LabResult>();
}
