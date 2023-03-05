using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    public interface IPermissionService
    {
        bool CheckAuthorization(int a_roleId, int a_permissionId);
    }
}
