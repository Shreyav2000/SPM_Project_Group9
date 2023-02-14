using System;
using System.Collections.Generic;
using System.Text;

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class PharmacyParse
    {
        public string TransId { get; set; }
        public string RequestId { get; set; }
        public string TransAuthor { get; set; }
        public DateTime? TransDate { get; set; }
    }
}
