using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Models;

public partial class UserPermissions
{

    public string Username { get; set; } = null!;

    public string Permission { get; set; } = null!;

}
