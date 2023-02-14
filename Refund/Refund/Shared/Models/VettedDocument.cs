using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class VettedDocument
    {
        public string DocumentGuid { get; set; }
        public string RequestId { get; set; }
        public string VetterId { get; set; }
        public decimal? AmountApproved { get; set; }
        public bool? NhisCovered { get; set; }
        public string DocumentType { get; set; }
        public DateTime? DocumentDate { get; set; }
        public bool? ComparableMarket { get; set; }
        public int? Satisfy { get; set; }
        public string Comments { get; set; }
    }
}
