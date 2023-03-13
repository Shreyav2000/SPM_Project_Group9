using HealthCare.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    public interface IUserRoleService
    {
        Task<bool> AddUserRole(UserRole userRole);
        Task<bool> DeleteUserRole(int roleId);
       // Task<User> ValidateToken(string token, string signingKey, int expiryMinutes);
    }
}
