using HealthCare.Shared.Enums;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Misc;
using HealthCare.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
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
        private readonly Methods.Validator m_validator;
        public TokensController(IAuthService a_authService, ITokenService tokenService,IPermissionService a_permission)
        {
            m_authService = a_authService;
            m_validator = new Methods.Validator(tokenService, a_permission);
        }

        [HttpPost, Route("authenticate")]
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
        [Authorize]
        [HttpPost, Route("register")]
        public async Task<IActionResult> SetupAccount(User a_userData)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;

            string? validationResult = m_validator.Validate(token, 22);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (a_userData != null && a_userData.Username != null && a_userData.Password != null)
            {
                if (m_authService.AccountStatus(null, a_userData.Username) != Shared.Enums.AuthEnums.Registered)
                {
                    var user = m_authService.CreateUser(a_userData);

                    if (user != AuthEnums.Successful && user != AuthEnums.Registered)
                    {
                        return BadRequest("Invalid credentials");
                    }
                    else if (user == AuthEnums.Registered)
                    {
                        return BadRequest("User with the same username already exists");
                    }
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
