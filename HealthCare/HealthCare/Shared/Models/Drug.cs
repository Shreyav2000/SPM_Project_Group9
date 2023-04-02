using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCare.Shared.Models;

public partial class Drug
{
    public string DrugId { get; set; } = string.Empty;

    public string? Drugname { get; set; }
    public decimal? Price { get; set; }
    public bool OutOfService { get; set; }
    public bool Refill { get; set; }

    public void SetId(string a_drugId)
    {
        DrugId= a_drugId;
    }
}
