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
    /// Service for managing users.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly HealthcareContext m_context;
        private readonly ILogger<UserService> m_logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger.</param>
        public UserService(HealthcareContext context, ILogger<UserService> logger)
        {
            m_context = context;
            m_logger = logger;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await m_context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Gets the last login time for the user with the specified username.
        /// </summary>
        /// <param name="username">The username of the user to get the last login time for.</param>
        /// <returns>The last login time for the user with the specified username.</returns>
        public async Task<List<User>> GetLastLoginTime()
        {
            try
            {
                var user = await m_context.Users.Where(u => u.LastLogin != null).ToListAsync();

                if (user == null)
                {
                    m_logger.LogWarning($"No Logged in or Recent users found.");
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, $"Failed to get last login time for user.");
                return null;
            }
        }


        /// <summary>
        /// Gets the lusers with their permissions
        /// </summary>
        /// <returns>List of users with their permission capabilities</returns>
        public async Task<List<UserPermissions>> GetuserPermissions()
        {
            try
            {
                List<UserPermissions> userPermissionsList = new List<UserPermissions>();
                var users = await m_context.Users.ToListAsync();
                var permissions = await m_context.Permissions.ToListAsync();

                foreach (var user in users)
                {
                    foreach (var permission in permissions)
                    {
                        if (permission.PermissionId == user.RoleId)
                        {
                            userPermissionsList.Add(new UserPermissions { Username = user.Username, Permission = permission.PermissionName });
                        }
                    }
                }

                return userPermissionsList;
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, $"Failed to get user permissions.");
                return null;
            }
        }

    }
}