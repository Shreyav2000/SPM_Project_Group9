using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class RefundUser
    {
        public int UserId { get; set; }
        public string Group { get; set; }
        public string Role { get; set; }
        public bool Transcriber { get; set; }
        public bool Requester { get; set; }
    }
}
