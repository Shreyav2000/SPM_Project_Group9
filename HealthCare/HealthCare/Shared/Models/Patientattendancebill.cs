using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Patientattendancebill
{
    public int BranchId { get; set; }

    public long BillId { get; set; }

    public int ConsultId { get; set; }
}
