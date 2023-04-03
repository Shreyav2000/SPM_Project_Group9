using HealthCare.Server.Methods;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
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
        private readonly IDoctorService m_doctorService;
        private readonly ITokenService m_tokenService;
        private readonly IComplaintService m_complaintService;

        public StaffController(IStaffService a_staffService, ITokenService a_tokenService, IPermissionService a_permission, IDoctorService doctorService, ITokenService tokenService, IComplaintService a_complaintService)
        {
            m_service = a_staffService;
            m_validator = new Methods.Validator(a_tokenService, a_permission);
            m_doctorService = doctorService;
            m_tokenService = tokenService;
            m_complaintService = a_complaintService;
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
        /// <summary>
        /// Endpoint save consulting session between a doctor and a patient
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpGet, Route("complaints")]
        public async Task<ActionResult<List<Complaint>>> GetComplaints()
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 34);
            int? doctor = m_tokenService.GetUserIdFromToken(token);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            return await m_complaintService.GetComplaint();
        }
        /// <summary>
        /// Endpoint save consulting session between a doctor and a patient
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpPost, Route("records/session")]
        public async Task<ActionResult> SaveRecord(SessionObject a_session)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 2);
            int? doctor = m_tokenService.GetUserIdFromToken(token);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (await m_doctorService.SubmitSession(a_session, (int)doctor))
                return Ok("Success");

            return BadRequest("Error occurred");
        }

        /// <summary>
        /// Endpoint get consulting session between a doctor and a patient
        /// </summary>
        /// <param name="a_sessionId"></param>
        [Authorize]
        [HttpGet, Route("records/session/{a_sessionId}")]
        public async Task<ActionResult<SessionObject>> GetRecord(int a_sessionId)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 3);
            int? doctor = m_tokenService.GetUserIdFromToken(token);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            return await m_doctorService.GetSession(a_sessionId);
        }
        /// <summary>
        /// Endpoint update consulting session between a doctor and a patient
        /// </summary>
        /// <param name="a_sessionId"></param>
        [Authorize]
        [HttpPut, Route("records/session")]
        public async Task<ActionResult> UpdateRecord(SessionObject a_session)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 3);
            int? doctor = m_tokenService.GetUserIdFromToken(token);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (await m_doctorService.UpdateSession(a_session,doctor))
                return Ok("success");

            return BadRequest("Error occurred, session not updated");
        }
    }
}
