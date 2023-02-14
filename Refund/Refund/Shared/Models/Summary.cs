using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Shared.Models
{
    public class Summary
    {
        public int approvedDocuments { get; set; }
        public int declinedDocuments { get; set; }
        public int protests { get; set; }
        public double totalApproved { get; set; }
        public List<DataItem> approved { get; set; }
        public List<DataItem> declined { get; set; }
        public List<CommonDoc> common { get; set; }
        public List<TopRefunded> topRefundeds { get; set; }
    }
}
