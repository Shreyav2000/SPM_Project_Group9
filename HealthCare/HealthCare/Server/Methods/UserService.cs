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
    }
}