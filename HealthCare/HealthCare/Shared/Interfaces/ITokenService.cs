using HealthCare.Shared.Enums;
using HealthCare.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    public interface ITokenService
    {
        AuthEnums ValidateToken(string a_token);
        string GenerateToken(User a_user);
        int? GetRoleFromToken(string a_token);
        int? GetUserIdFromToken(string a_token);
        string? GetUserNameFromToken(string a_token);
        
    }
}
