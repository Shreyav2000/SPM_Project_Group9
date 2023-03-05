using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Labtest
{
    public int LabTestId { get; set; }

    public string? LabTest1 { get; set; }

    public int? ExamId { get; set; }

    public int? SpecimenId { get; set; }

    public string? Units { get; set; }

    public string? VRange { get; set; }

    public decimal? AmtPrice { get; set; }

    public string? Gdrg { get; set; }

    public string? Icd { get; set; }

    public int? Category { get; set; }

    public decimal? Pricead { get; set; }

    public string? Agecat { get; set; }

    public string? Gendercat { get; set; }

    public int? Specid { get; set; }

    public int InvestigationCategoryId { get; set; }

    public bool? ShowInDiabetesCarePlan { get; set; }

    public bool? IsActive { get; set; }

    public int? ReportId { get; set; }

    public int? SortOrder { get; set; }

    public int? UserId { get; set; }

    public DateTime? TransDate { get; set; }

    public bool? Flg { get; set; }

    public virtual Investigationcategory InvestigationCategory { get; set; } = null!;
}
