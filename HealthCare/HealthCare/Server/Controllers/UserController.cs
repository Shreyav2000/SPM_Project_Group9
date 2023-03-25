using Azure.Core;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace HealthCare.Server.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Methods.Validator m_validator;
        private readonly IUserService m_service;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="tokenService">The token service.</param>
        /// <param name="permissionService">The permission service.</param>
        public UserController(IUserService userService, ITokenService tokenService, IPermissionService permissionService)
        {
            m_service = userService;
            m_validator = new Methods.Validator(tokenService, permissionService);
        }

        /// <summary>
        /// Gets the last login time for a user.
        /// </summary>
        /// <param name="username">The username of the user to get the last login time for.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        /// <remarks>
        /// If the user is not authenticated, a 401 Unauthorized response is returned.
        /// If the user does not have the necessary permissions, a 403 Forbidden response is returned.
        /// If the user is found and the last login time is retrieved successfully, a 200 OK response is returned with the last login time.
        /// If the user is not found, a 404 Not Found response is returned with an error message.
        /// If an error occurs, a 400 Bad Request response is returned with an error message.
        /// </remarks>
        [Authorize]
        [HttpGet("lastlogin")]
        public async Task<ActionResult<List<User>>> GetLastLoginTime()
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 32);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            var user = await m_service.GetLastLoginTime();
            if (user == null)
                return BadRequest($"Try again later");

            return user;
        }
    }
}
