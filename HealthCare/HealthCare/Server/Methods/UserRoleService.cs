using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// Service for managing user roles.
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        private readonly HealthcareContext m_context;
        private readonly ILogger<UserRoleService> m_logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger.</param>
        public UserRoleService(HealthcareContext context, ILogger<UserRoleService> logger)
        {
            m_context = context;
            m_logger = logger;
        }

        /// <summary>
        /// Adds a new user role to the database.
        /// </summary>
        /// <param name="userRole">The user role to add.</param>
        /// <returns><c>true</c> if the user role was added successfully,
        /// otherwise <c>false</c>.</returns>
        public async Task<bool> AddUserRole(UserRole userRole)
        {
            try
            {
                m_context.UserRoles.AddAsync(userRole);
                int i = await m_context.SaveChangesAsync();
                if (i > 0)
                    return true;
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, $"Failed to add user role with Id {userRole.RoleName}.");

            }

            return false;
        }

        /// <summary>
        /// Deletes a user role from the database.
        /// </summary>
        /// <param name="roleId">The ID of the user role to delete.</param>
        /// <returns><c>true</c> if the user role was deleted successfully,
        /// otherwise <c>false</c>.</returns>
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
                int i = await m_context.SaveChangesAsync();
                if (i > 0)
                    return true;
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, $"Failed to delete user role with Id {roleId}.");

            }

            return false;
        }

        /// <summary>
        /// Gets a list of all user roles in the system.
        /// </summary>
        /// <returns>A list of user roles.</returns>
        public async Task<List<UserRole>> GetAllUserRoles()
        {
            try
            {
                return await m_context.UserRoles.ToListAsync();
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, "Failed to get user roles from the database.");
                return null;
            }
        }

        /// <summary>
        /// Updates an existing user role in the database.
        /// </summary>
        /// <param name="userRole">The user role to update.</param>
        /// <returns><c>true</c> if the user role was updated successfully,
        /// otherwise <c>false</c>.</returns>
        public async Task<bool> UpdateUserRole(UserRole userRole)
        {
            try
            {
                var existingUserRole = await m_context.UserRoles.FindAsync(userRole.RoleId);

                if (existingUserRole == null)
                {
                    m_logger.LogWarning($"User role with Id {userRole.RoleId} was not found.");
                    return false;
                }

                existingUserRole.RoleName = userRole.RoleName;

                int i = await m_context.SaveChangesAsync();
                if (i > 0)
                    return true;
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, $"Failed to update user role with Id {userRole.RoleId}.");

            }

            return false;
        }


    }
}
