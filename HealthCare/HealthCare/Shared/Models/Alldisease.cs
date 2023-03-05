using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Alldisease
{
    public int DiseaseId { get; set; }

    public string? DiseaseName { get; set; }

    public int? CatId { get; set; }

    public bool? IsActive { get; set; }

    public long? NoDays { get; set; }
}
