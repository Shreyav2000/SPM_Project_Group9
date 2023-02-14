using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Shared.Models
{
    public class SupportDocument
    {
        public string item { get; set; }
        public string reportID { get; set; }
        public string documentID { get; set; }
        public string documentType { get; set; }
        public string companyName { get; set; }
        public double? amount { get; set; }
        public string clientName { get; set; }
        public DateTime date { get; set; }
        public bool? vetState { get; set; }
        public string documentGUID { get; set; }
        public string attachment { get; set; }
        public string parseID { get; set; }
        public VettedDocument vetting { get; set; }
    }
}
