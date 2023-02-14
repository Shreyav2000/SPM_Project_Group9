using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class RefundSupportDocument
    {
        public string DocumentGuid { get; set; }
        public string ReportId { get; set; }
        public string DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string CompanyName { get; set; }
        public decimal? Amount { get; set; }
        public string ClientName { get; set; }
        public DateTime? Date { get; set; }
        public string Prescriber { get; set; }
        public string Requester { get; set; }
        public string Attachment { get; set; }
        public bool? VetState { get; set; }
        public string Item { get; set; }
        public string ParseId { get; set; }
        public bool external { get; set; }

    }
}
