using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCare.Shared.Models;

public partial class DrugStock
{
    public string DrugId { get; private set; } = string.Empty;

    public int Quantity{ get; set; }

    public void SetId(string a_drugId)
    {
        DrugId= a_drugId;
    }
}
