using System;
using System.Collections.Generic;

#nullable disable

namespace KNUST_Medical_Refund.Shared.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string HashedPassword { get; set; }
        public string Description { get; set; }
        public string PasswordHint { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsEdge { get; set; }
        public int? LegacyStaffId { get; set; }
        public int FailedLoginCount { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
