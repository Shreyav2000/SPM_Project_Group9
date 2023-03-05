using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Labresultsdetailssection
{
    public int RecNum { get; set; }

    public int? Consid { get; set; }

    public int? LabSessionId { get; set; }

    public int? LabTestId { get; set; }

    public int? LocalId { get; set; }

    public int? Flvid { get; set; }

    public int? Slid { get; set; }

    public int? Tlid { get; set; }

    public string? Comment { get; set; }

    public string? TestRange { get; set; }
}
