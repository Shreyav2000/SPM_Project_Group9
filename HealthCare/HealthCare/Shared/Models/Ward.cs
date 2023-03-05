using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Ward
{
    public int WardId { get; set; }

    public string? WardName { get; set; }

    public int? CRoomId { get; set; }

    public int? WardTypeId { get; set; }
}
