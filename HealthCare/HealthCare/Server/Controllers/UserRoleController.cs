using Azure.Core;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace HealthCare.Server.Controllers
{
    /// <summary>
    /// Controller for managing user roles.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly Methods.Validator m_validator;
        private readonly IUserRoleService m_service;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleController"/> class.
        /// </summary>
        /// <param name="a_userRoleService">The user role service.</param>
        /// <param name="a_tokenService">The token service.</param>
        /// <param name="a_permissionService">The permission service.</param>
        public UserRoleController(IUserRoleService a_userRoleService, ITokenService a_tokenService, IPermissionService a_permissionService)
        {
            m_service = a_userRoleService;
            m_validator = new Methods.Validator(a_tokenService, a_permissionService);
        }


        /// <summary>
        /// Gets all user roles from the database.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the user roles.</returns>
        /// <remarks>
        /// If the user is not authenticated, a 401 Unauthorized response is returned.
        /// If the user does not have the necessary permissions, a 403 Forbidden response is returned.
        /// If the user roles were retrieved successfully, a 200 OK response is returned with the user roles.
        /// If an error occurs, a 400 Bad Request response is returned with an error message.
        /// </remarks>
        [Authorize]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUserRoles()
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 33);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            var userRoles = await m_service.GetAllUserRoles();
            if (userRoles != null)
                return Ok(userRoles);

            return BadRequest("Error occurred, please try again later");
        }


        /// <summary>
        /// Adds a new user role to the database.
        /// </summary>
        /// <param name="userRole">The user role to add.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        /// <remarks>
        /// If the user is not authenticated, a 401 Unauthorized response is returned.
        /// If the user does not have the necessary permissions, a 403 Forbidden response is returned.
        /// If the user role was added successfully, a 200 OK response is returned with a message.
        /// If an error occurs, a 400 Bad Request response is returned with an error message.
        /// </remarks>
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

        /// <summary>
        /// Deletes a user role from the database.
        /// </summary>
        /// <param name="roleId">The ID of the user role to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        /// <remarks>
        /// If the user is not authenticated, a 401 Unauthorized response is returned.
        /// If the user does not have the necessary permissions, a 403 Forbidden response is returned.
        /// If the user role was deleted successfully, a 200 OK response is returned with a message.
        /// If an error occurs, a 400 Bad Request response is returned with an error message.
        /// </remarks>
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

        /// <summary>
        /// Updates an existing user role in the database.
        /// </summary>
        /// <param name="userRole">The updated user role.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        /// <remarks>
        /// If the user is not authenticated, a 401 Unauthorized response is returned.
        /// If the user does not have the necessary permissions, a 403 Forbidden response is returned.
        /// If the user role was updated successfully, a 200 OK response is returned with a message.
        /// If an error occurs, a 400 Bad Request response is returned with an error message.
        /// </remarks>
        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUserRole(UserRole userRole)
        {
            // Validate user's token and permissions
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 35);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            // Attempt to update user role in the database
            if (await m_service.UpdateUserRole(userRole))
                return Ok("User role updated successfully");

            return BadRequest("Error occurred, please try again later");
        }


    }
}
