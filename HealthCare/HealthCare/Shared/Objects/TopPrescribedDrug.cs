using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Objects
{
    public class TopPrescribedDrug
    {
        public string Drug { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
