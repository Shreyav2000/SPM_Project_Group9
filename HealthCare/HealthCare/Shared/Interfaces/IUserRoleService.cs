using HealthCare.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    /// <summary>
    /// Defines methods for user role management.
    /// </summary
    public interface IUserRoleService
    {

        /// <summary>
        /// Gets a list of all user roles in the system.
        /// </summary>
        /// <returns>A list of user roles.</returns>
        Task<List<UserRole>> GetAllUserRoles();

        /// <summary>
        /// Adds a new user role to the system.
        /// </summary>
        /// <param name="userRole">The user role to add.</param>
        /// <returns>A boolean indicating whether the user role was added successfully.</returns>
        Task<bool> AddUserRole(UserRole userRole);

        /// <summary>
        /// Deletes an existing user role from the system.
        /// </summary>
        /// <param name="roleId">The ID of the user role to delete.</param>
        /// <returns>A boolean indicating whether the user role was deleted successfully.</returns>
        Task<bool> DeleteUserRole(int roleId);

        /// <summary>
        /// Updates an existing user role in the system.
        /// </summary>
        /// <param name="userRole">The user role to update.</param>
        /// <returns>A boolean indicating whether the user role was updated successfully.</returns>
        Task<bool> UpdateUserRole(UserRole userRole);


    }
}
