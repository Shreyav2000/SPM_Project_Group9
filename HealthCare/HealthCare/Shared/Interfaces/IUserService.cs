using HealthCare.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    /// <summary>
    /// Defines methods for user management.
    /// </summary>
    public interface IUserService
    {
        Task<User> GetUserByUsername(string username);


        /// <summary>
        /// Gets the last login time for the user with the specified username.
        /// </summary>
        /// <returns>The last login time for the user with the specified username.</returns>
        Task<List<User>> GetLastLoginTime();

        Task<List<UserPermissions>> GetuserPermissions();

    }
}