using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class PreprocessedDate
    {
        public string RequestId { get; set; }
        public DateTime? DatePreprocessed { get; set; }
    }
}
