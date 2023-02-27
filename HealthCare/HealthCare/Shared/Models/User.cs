using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual UserRole Role { get; set; } = null!;
}
