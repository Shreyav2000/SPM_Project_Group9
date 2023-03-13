using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Shared.Models;

namespace HealthCare.Shared.Objects
{
    public class DrugModel
    {
        public string DrugId { get; set; }
        public Drug Drug { get; set; }
        public DrugStock Stock { get; set; }
    }
}
