using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Shared.Models
{
    public class Protest
    {
        public string protestID { get; set; }
        public string requestID { get; set; }
        public List<SupportDocument> attachments { get; set; }
        public DateTime? dateprocessed { get; set; }
        public DateTime? dateprotested { get; set; }
        public bool? open { get; set; }
        public double AmountApproved { get; set; }
        public string comments { get; set; }
    }
}
