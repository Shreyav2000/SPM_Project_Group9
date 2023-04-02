using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public virtual ICollection<User> Users { get; } = new List<User>();

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();
   // public virtual int UserId { get; set; }
}


