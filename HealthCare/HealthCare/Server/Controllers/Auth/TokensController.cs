using HealthCare.Shared.Enums;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthCare.Server.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IAuthService m_authService;

        public TokensController(IAuthService a_authService)
        {
            m_authService = a_authService;
        }

        [HttpPost,Route("authenticate")]
        public async Task<IActionResult> AuthUser(User a_userData)
        {

            if (a_userData != null && a_userData.Username != null && a_userData.Password != null)
            {
                var user = await m_authService.Authenticate(a_userData.Username, a_userData.Password);

                if (user == null)
                    return BadRequest("Invalid credentials");
                return Ok(user.ToString());
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost, Route("register")]
        public async Task<IActionResult> SetupAccount(User a_userData)
        {
            if (a_userData != null && a_userData.Username != null && a_userData.Password != null)
            {
                if (m_authService.AccountStatus(null, a_userData.Username) != Shared.Enums.AuthEnums.Registered)
                {

                    var user = m_authService.CreateUser(a_userData);

                    if (user != AuthEnums.Successful)
                        return BadRequest("Invalid credentials");
                }
                return Ok("User created successfully");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
