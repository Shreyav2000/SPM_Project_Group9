using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Staff
{
    public int Staffid { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string? City { get; set; }

    public string? Province { get; set; }

    public string? Street { get; set; }

    public string? Telephone2 { get; set; }

    public string? Telephone1 { get; set; }

    public byte[]? Picture { get; set; }

    public DateTime? DateRegistered { get; set; }

    public string? Email { get; set; }

    public int? BranchId { get; set; }
}
