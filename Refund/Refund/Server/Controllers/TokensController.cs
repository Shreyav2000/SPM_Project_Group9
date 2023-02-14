using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KNUST_Medical_Refund.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly KNUSTMISContext _context;

        public TokensController(IConfiguration config, KNUSTMISContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthUser _userData)
        {

            if (_userData != null && _userData.Username != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Username, _userData.Password);

                if (user != null)
                {
                    AuthUser tokenReturn = new AuthUser();
                    tokenReturn.Username = user.UserName;
                    tokenReturn.UserId = user.UserId;
                    tokenReturn.Fullname = user.FullName;
                    tokenReturn.Role = await _context.RefundUsers.Where(uid => uid.UserId == user.UserId).Select(r => r.Role).FirstOrDefaultAsync();
                    tokenReturn.Group = await _context.RefundUsers.Where(uid => uid.UserId == user.UserId).Select(r => r.Group).FirstOrDefaultAsync();
                   
                    //create claims details based on the user information
                    var claims = new[] {
                    //new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("userid", user.UserId.ToString()),
                    new Claim("password", _userData.Password),
                    new Claim("role", tokenReturn.Role),
                    new Claim("group", tokenReturn.Group),

                   // new Claim("access_level",user.AccessLevel),
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(14), signingCredentials: signIn);
                    //   Console.WriteLine(token);
                    tokenReturn.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                    tokenReturn.expiry = token.ValidTo.ToUniversalTime();

                    return Ok(tokenReturn);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUser(string username, string password)
        {
            string pa = await _context.Users.Where(u => u.UserName == username).Select(u => u.HashedPassword).FirstAsync();
            bool verified = BCrypt.Net.BCrypt.Verify(password, pa);
            if (verified)
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            }
            else
            {
                return null;
            }
        }
    }
}