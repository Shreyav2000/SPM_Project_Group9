using HealthCare.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Objects
{
    public class DrugAnalysis
    {
        public List<Expiryitem> ExpiredDrugs { get; set; }
        public string MostPurchasedDrug { get; set; }
        public string MostPrescribedDrug { get; set; }
        public List<Drug> ReplenishedDrugs { get; set; }
        public string? MostExpensiveDrug { get; set; }
    }
}
