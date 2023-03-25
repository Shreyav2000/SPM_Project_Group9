using HealthCare.Shared.Enums;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthCare.Server.Methods
{
    public class AuthRepo : IAuthService
    {
        private readonly HealthcareContext m_context;
        private readonly ITokenService m_tokenService;

        public AuthRepo(HealthcareContext a_context,ITokenService a_tokenService)
        {
            m_context = a_context;
            m_tokenService = a_tokenService;
        }

        /// <summary>
        /// Returns the status of a user account
        /// </summary>
        /// <returns>AuthEnums</returns>
        public AuthEnums AccountStatus(int? a_id,string? a_username)
        {
            if(m_context.Users.Any(i => i.UserId == a_id || i.Username == a_username))
            {
                return AuthEnums.Found;
            }
            else
            {
                return AuthEnums.NotFound;
            }
        }
        /// <summary>
        /// Autenticates a user and returns a tokenized string
        /// </summary>
        /// <param name="a_username"></param>
        /// <param name="a_password"></param>
        /// <returns>strings</returns>
        public async Task<string> Authenticate(string a_username, string a_password)
        {
            var user = await GetUserAsync(a_username, a_password);

            if(user == null)
            {
                return null;
            }
            string Token = m_tokenService.GenerateToken(user);
            return m_tokenService.GenerateToken(user);
        }

        /// <summary>
        /// Creates a new user account
        /// </summary>
        /// returns AuthEnums.successful if the account was created successfully
        public AuthEnums CreateUser(User a_user)
        {
            if (AccountStatus(null, a_user.Username) == AuthEnums.Found)
                return AuthEnums.Registered;

            a_user.Role = m_context.UserRoles.First(i => i.RoleId== a_user.RoleId);
            a_user.Password = BCrypt.Net.BCrypt.HashPassword(a_user.Password);
            m_context.Users.Add(a_user);

            var result = m_context.SaveChangesAsync();
            result.Wait();
            if(result.Result > 0)
            {
                return AuthEnums.Successful;
            }
            return AuthEnums.Error;
        }

        /// <summary>
        /// Deletes a user account
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteUser(int a_id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            return m_context.Users.First(i => i.UserId == id);
        }

        /// <summary>
        /// Gets the list of all users
        /// </summary>
        /// <returns>List<Usert></returns>
        public List<User> GetUsers()
        {
            return m_context.Users.ToList();
        }
        /// <summary>
        /// Locks or disables a user account
        /// </summary>
        public void LockUser()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates a user account
        /// </summary>
        public void UpdateUser()
        {
            throw new NotImplementedException();
        }
        private async Task<User> GetUserAsync(string username, string password)
        {
            try
            {
                string pa = await m_context.Users.Where(u => u.Username == username).Select(u => u.Password).FirstAsync();
                bool verified = BCrypt.Net.BCrypt.Verify(password, pa);
                if (verified)
                {
                    return await m_context.Users.FirstAsync(u => u.Username == username);
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                //possible is user not found 
                return null;
            }
        }
    }
}
