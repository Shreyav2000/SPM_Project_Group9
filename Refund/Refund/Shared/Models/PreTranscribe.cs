using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class PreTranscribe
    {
        public string PharmId { get; set; }
        public string Drugname { get; set; }
        public double? Dosage { get; set; }
        public int? Unit { get; set; }
        public int? Frequency { get; set; }
        public int? Duration { get; set; }
        public int? Route { get; set; }
        public string Notes { get; set; }
        public string TransId { get; set; }
        public string DocumentGuid { get; set; }
        public bool? Purchased { get; set; }
        public string RequestId { get; set; }
        public DateTime? DatePurchased { get; set; }
    }
}
