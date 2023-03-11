using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Models
{
    public class DrugModel
    {
        public Drug Drug { get; set; }
        public Drugitem Drugitem { get; set; }
        public DrugStock Stock { get; set; }
    }
}
