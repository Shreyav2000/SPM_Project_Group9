using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class AiprocessedDocument
    {
        public bool? DocumentId { get; set; }
        public string DocIdstate { get; set; }
        public bool? Type { get; set; }
        public string TypeState { get; set; }
        public bool? Amount { get; set; }
        public string AmountState { get; set; }
        public bool? Client { get; set; }
        public string ClientState { get; set; }
        public bool? Date { get; set; }
        public string DateState { get; set; }
        public bool? Duplicate { get; set; }
        public bool? Institution { get; set; }
        public string InstitutionState { get; set; }
        public string ExistingId { get; set; }
        public string ParseId { get; set; }
        public string ReportId { get; set; }
    }
}
