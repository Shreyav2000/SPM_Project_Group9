using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class Category
    {
        public string Id { get; set; }
        public string StaffCategory { get; set; }
        public string Prefix { get; set; }
        public string Desciption { get; set; }
    }
}
