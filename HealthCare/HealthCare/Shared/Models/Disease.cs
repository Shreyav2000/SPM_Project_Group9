using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Disease
{
    public int DiseaseId { get; set; }

    public string DiseaseName { get; set; } = null!;

    public string Description { get; set; } = null!;
}
