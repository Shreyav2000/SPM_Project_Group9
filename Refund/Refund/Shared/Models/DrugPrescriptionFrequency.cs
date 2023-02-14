using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class DrugPrescriptionFrequency
    {
        public int Id { get; set; }
        public string FrequencyName { get; set; }
        public int? FrequencyValue { get; set; }
    }
}
