using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Department
{
    public string? Deptname { get; set; }

    public int? DepType { get; set; }

    public int Deptid { get; set; }

    public int? DeptHead { get; set; }

    public bool? IsDepartment { get; set; }

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();
}
