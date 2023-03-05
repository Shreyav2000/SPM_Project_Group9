using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCare.Shared.Models;

public partial class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual UserRole Role { get; set; } = null!;
}
