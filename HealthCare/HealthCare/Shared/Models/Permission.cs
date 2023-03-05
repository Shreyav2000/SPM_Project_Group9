using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public virtual ICollection<UserRole> Roles { get; } = new List<UserRole>();
}
