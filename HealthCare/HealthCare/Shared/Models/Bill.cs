using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Bill
{
    public string BillId { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public int StaffId { get; set; }

    public DateTime BillDate { get; set; }

    public int? Deptid { get; set; }

    public virtual ICollection<Billdetail> Billdetails { get; } = new List<Billdetail>();

    public virtual Department? Dept { get; set; }
}
