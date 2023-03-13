using Azure.Core;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace HealthCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly Methods.Validator m_validator;
        private readonly IUserRoleService m_service;

        public UserRoleController(IUserRoleService a_userRoleService, ITokenService a_tokenService, IPermissionService a_permissionService)
        {
            m_service = a_userRoleService;
            m_validator = new Methods.Validator(a_tokenService, a_permissionService);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddUserRole(UserRole userRole)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 30);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (await m_service.AddUserRole(userRole))
                return Ok("User role added successfully");

            return BadRequest("Error occurred, please try again later");
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUserRole(int roleId)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 31);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (await m_service.DeleteUserRole(roleId))
                return Ok("User role deleted successfully");

            return BadRequest("Error occurred, please try again later");
        }
    }
}
