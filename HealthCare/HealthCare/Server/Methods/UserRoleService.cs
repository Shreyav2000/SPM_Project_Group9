using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Server.Methods
{
    public class UserRoleService : IUserRoleService
    {
        private readonly HealthcareContext m_context;
        private readonly ILogger<UserRoleService> m_logger;

        public UserRoleService(HealthcareContext context, ILogger<UserRoleService> logger)
        {
            m_context = context;
            m_logger = logger;
        }

        public async Task<bool> AddUserRole(UserRole userRole)
        {
            try
            {
                m_context.UserRoles.AddAsync(userRole);
                await m_context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, $"Failed to add user role with Id {userRole.RoleName}.");
                return false;
            }
        }

        public async Task<bool> DeleteUserRole(int roleId)
        {
            try
            {
                var userRole = await m_context.UserRoles.FindAsync(roleId);

                if (userRole == null)
                {
                    m_logger.LogWarning($"User role with Id {roleId} was not found.");
                    return false;
                }

                m_context.UserRoles.Remove(userRole);
                await m_context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, $"Failed to delete user role with Id {roleId}.");
                return false;
            }
        }

    }
}