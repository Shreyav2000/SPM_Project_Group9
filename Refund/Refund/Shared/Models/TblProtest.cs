using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class TblProtest
    {
        public string ProtestId { get; set; }
        public string RequestId { get; set; }
        public DateTime? DateProtested { get; set; }
        public bool? Open { get; set; }
        public DateTime? DateProcessed { get; set; }
        public string Comments { get; set; }
        public decimal? AmountApproved { get; set; }
    }
}
