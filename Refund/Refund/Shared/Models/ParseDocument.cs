using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Shared.Models
{
    public class ParseDocumentDD
    {
        public string parseID { get; set; }
        public bool? documentID { get; set; }
        public string documentState { get; set; }
        public bool? type { get; set; }
        public string typeState { get; set; }
        public bool? amount { get; set; }
        public string amountState { get; set; }
        public bool? client { get; set; }
        public string clientState { get; set; }
        public bool? date { get; set; }
        public string dateState { get; set; }
        public bool? duplicate { get; set; }
        public string existingID { get; set; }
        public bool? institution { get; set; }
        public string institutionState { get; set; }
        public string ReportId { get; set; }
    }
}
