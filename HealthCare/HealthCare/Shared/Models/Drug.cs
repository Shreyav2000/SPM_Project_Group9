using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Drug
{
    public int DrugId { get; set; }

    public string DrugName { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public string? Dosage { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; } = new List<Prescription>();

    public virtual ICollection<SoldDrug> SoldDrugs { get; } = new List<SoldDrug>();
}
