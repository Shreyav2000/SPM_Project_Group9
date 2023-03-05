using Azure.Core;
using HealthCare.Shared.Enums;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthCare.Server.Methods
{
    public class TokenService : ITokenService
    {
        private IConfiguration m_configuration { get; }
        public TokenService(IConfiguration a_configuration)
        {
            m_configuration = a_configuration;
        }

        /// <summary>
        /// Validates a user access token
        /// </summary>
        /// <returns>AuthEnums.Valid if token is a valid and unexpired token</returns>
        public AuthEnums ValidateToken(string a_token)
        {
            try
            {
                string token = a_token.Substring(6).Trim();
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken.ValidFrom <= DateTime.Now && jwtToken.ValidTo > DateTime.Now)
                    return AuthEnums.Valid;
                return AuthEnums.Expired;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return AuthEnums.Invalid;
            }
        }
        /// <summary>
        /// Generates an access token based on the user
        /// </summary>
        /// <returns>A tokenized string</returns>
        public string GenerateToken(User a_user)
        {
            try
            {
                //create claims details based on the user information
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("userid", a_user.UserId.ToString()),
                    new Claim("role",a_user.RoleId.ToString()),
                    new Claim("username", a_user.Username),
                   };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_configuration["Jwt:Key"]!));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(m_configuration["Jwt:Issuer"], m_configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(14), signingCredentials: signIn);
                return $"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        ///Deserializes the token string if valid to return the specified user role id in the token string
        /// </summary>
        /// <param name="a_token"></param>
        /// <returns>RoleId</returns>
        public int? GetRoleFromToken(string a_token)
        {
            if (ValidateToken(a_token) == AuthEnums.Valid)
            {
                string token = a_token.Substring(6).Trim();
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                return int.Parse(jwtToken.Claims.First(claim => claim.Type == "role").Value);
            }
            return null;
        }
        /// <summary>
        ///Deserializes the token string if valid to return the specified user id in the token string
        /// </summary>
        /// <param name="a_token"></param>
        /// <returns>UserId</returns>
        public int? GetUserIdFromToken(string a_token)
        {
            if (ValidateToken(a_token) == AuthEnums.Valid)
            {
                string token = a_token.Substring(6).Trim();
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                return int.Parse(jwtToken.Claims.First(claim => claim.Type == "userid").Value);
            }
            return null;
        }
        /// <summary>
        ///Deserializes the token string if valid to return the specified username in the token string
        /// </summary>
        /// <param name="a_token"></param>
        /// <returns>Username</returns>
        public string? GetUserNameFromToken(string a_token)
        {
            if (ValidateToken(a_token) == AuthEnums.Valid)
            {
                string token = a_token.Substring(6).Trim();
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                return jwtToken.Claims.First(claim => claim.Type == "username").Value;
            }
            return null;
        }
    }
}
