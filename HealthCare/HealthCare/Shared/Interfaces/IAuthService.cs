using HealthCare.Shared.Enums;
using HealthCare.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    public interface IAuthService
    {
        User GetUser(int id);
        Task<string> Authenticate(string a_username, string a_password);
        List<User> GetUsers();
        AuthEnums CreateUser(User a_user);
        void UpdateUser();
        void DeleteUser(int a_id);
        void LockUser();
        AuthEnums AccountStatus(int? a_id,string? a_username);
    }
}
