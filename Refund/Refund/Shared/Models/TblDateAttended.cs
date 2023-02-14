using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class TblDateAttended
    {
        public string ReportId { get; set; }
        public DateTime? DateAttended { get; set; }
        public int CountId { get; set; }
    }
}
