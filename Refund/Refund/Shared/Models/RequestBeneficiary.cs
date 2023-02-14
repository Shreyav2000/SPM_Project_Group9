using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class RequestBeneficiary
    {
        public string RequestId { get; set; }
        public string Nhisnumber { get; set; }
        public string BeneficiaryFullname { get; set; }
        public int? BeneficiaryAge { get; set; }
        public string BeneficaryId { get; set; }
        public string HospitalNo { get; set; }
    }
}
