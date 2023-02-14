using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class RefundRequestInfo
    {
        public string RequestId { get; set; }
        public string ClaimantId { get; set; }
        public string Status { get; set; }
        public string VetterId { get; set; }
        public decimal? RequestedAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ProcessingDate { get; set; }
        public string BeneficiaryType { get; set; }
        public string BeneficiaryCategory { get; set; }
        public string HospitalAttended { get; set; }
        public string Reason { get; set; }
    }
}
