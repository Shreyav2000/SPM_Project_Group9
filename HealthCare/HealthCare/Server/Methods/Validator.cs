using HealthCare.Shared.Enums;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Misc;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace HealthCare.Server.Methods
{
    public class Validator
    {
        private readonly ITokenService m_tokenService;
        private readonly IPermissionService m_permission;

        public Validator(ITokenService tokenService, IPermissionService a_permission)
        {
            m_tokenService = tokenService;
            m_permission = a_permission;
        }
        public string? Validate(string a_token, int a_permissionId) {
            //Valids access token supplied
            var tokenStatus = m_tokenService.ValidateToken(a_token);
            if (tokenStatus != AuthEnums.Valid)
            {
                string response = tokenStatus == AuthEnums.Expired ? Constants.c_expiredToken : Constants.c_brokenToken;
                return response;
            }
            //Gets role id from the token
            int? roleId = m_tokenService.GetRoleFromToken(a_token);
            if (roleId == null)
                return Constants.c_unauthorzied;

            //Checks if user has the required permission to continue
            if (!m_permission.CheckAuthorization(roleId.Value, a_permissionId))
                return Constants.c_unauthorzied;

            return null;
        }
    }
}
