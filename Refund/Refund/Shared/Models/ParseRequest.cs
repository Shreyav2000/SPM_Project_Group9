using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Shared.Models
{
    public class Mainrequests
    {
        public List<requests> requests { get; set; }
    }
    public class requests
    {
        public image image { get; set; }
        public List<features> features { get; set; }
        public context imageContext { get; set; }
    }

    public class context
    {
        public List<string> languageHints { get; set; }
    }
    public class image
    {
        public source source { get; set; }
        public string content { get; set; }
    }
    public class source
    {
        public string imageUri { get; set; }
    }
    public class features
    {
        public string type { get; set; }
    }
}
