using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Diagnosis
{
    public string? Diagnosis1 { get; set; }

    public int DiagnosisId { get; set; }

    public int? Catid { get; set; }

    public string? Gdrg { get; set; }

    public string? Oth { get; set; }

    public decimal? Price { get; set; }

    public decimal? Pricead { get; set; }

    public string? Agecat { get; set; }

    public string? Gendercat { get; set; }

    public int? Specid { get; set; }
}
