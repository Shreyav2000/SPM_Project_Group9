using System;
using System.Collections.Generic;
using System.Text;

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class ClaimantInfo
    {
        public string ClaimantId { get; set; }
        public string Fullname { get; set; }
        public string Telephone { get; set; }
        public string Department { get; set; }
        public int? Age { get; set; }
        public string Knustid { get; set; }
        public string HospitalNo { get; set; }
    }
}
