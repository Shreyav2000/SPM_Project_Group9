using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCare.Shared.Models;

public partial class Expiryitem
{
    public string? ItemId { get; set; }

    public int Quantity { get; set; }

    public string? Shelf { get; set; }

    public DateTime? ManDate { get; set; }

    public DateTime? ExpDate { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Nid { get;}
    public DateTime DateModified { get; set; }

}
