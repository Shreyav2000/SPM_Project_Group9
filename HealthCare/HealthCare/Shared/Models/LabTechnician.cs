using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class LabTechnician
{
    public int TechnicianId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Address { get; set; }

    public virtual ICollection<LabResult> LabResults { get; } = new List<LabResult>();
}
