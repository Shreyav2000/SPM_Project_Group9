using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Server.Methods
{
    public class PermissionService : IPermissionService
    {
        private readonly HealthcareContext m_context;

        public PermissionService(HealthcareContext context)
        {
            m_context = context;
        }

        public bool CheckAuthorization(int a_roleId, int a_permissionId)
        {
            var matchingRolePermission = m_context.UserRoles
            .Where(ur => ur.RoleId == a_roleId)
            .SelectMany(ur => ur.Permissions)
            .SingleOrDefault(p => p.PermissionId == a_permissionId);

            return matchingRolePermission != null;
        }
    }
}
