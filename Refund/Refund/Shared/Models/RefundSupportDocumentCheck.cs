using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class RefundSupportDocumentCheck
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public string DocumentGuid { get; set; }
        public string Notes { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
