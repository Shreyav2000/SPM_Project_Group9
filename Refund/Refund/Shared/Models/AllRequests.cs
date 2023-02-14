using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Shared.Models
{
    public class AllRequests
    {
        public List<Request> processed { get; set; }
        public List<Request> unprocessed { get; set; }
    }
}
