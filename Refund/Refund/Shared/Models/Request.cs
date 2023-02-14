using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Shared.Models
{
    public class Request
    {
        public string ReportID { get; set; }
        public string Status { get; set; }
        public string Vetter { get; set; }
        public double RequestedAmount { get; set; }
        public double ApprovedAmount { get; set; }
        public DateTime Request_date { get; set; }
        public DateTime? Date_processed { get; set; }
        public string BeneficiaryType { get; set; }
        public string BeneficiaryCategory { get; set; }
        public ClaimantInfo claimant { get; set; }
        public RequestBeneficiary Beneficiary { get; set; }
        public string HospitalAttended { get; set; }
        public string Reason { get; set; }
        public List<SupportDocument> RefundItems { get; set; }
        public List<DateTime> DatesAttended { get; set; }
        public Protest protested { get; set; }
        public DateTime? preprocessedDate { get; set; }
        public List<PharmacyTranscribe> transcribes { get; set; }
    }
}
