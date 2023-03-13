using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace HealthCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {

        private readonly Methods.Validator m_validator;
        private readonly IStaffService m_service;

        public StaffController(IStaffService a_staffService, ITokenService a_tokenService, IPermissionService a_permission)
        {
            m_service = a_staffService;
            m_validator = new Methods.Validator(a_tokenService, a_permission);
        }
        /// <summary>
        /// Endpoint to look up a staff profile using their id
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpGet, Route("records/profile/{a_id}")]
        public ActionResult<List<MedHistory>> GetPatient(int a_id)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 7);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            var history = m_service.GetStaff(a_id);
            return Ok(history);
        }
    }
}
