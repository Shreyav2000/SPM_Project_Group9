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
    public class PatientController : ControllerBase
    {
        private readonly Methods.Validator m_validator;
        private readonly IPatientService m_service;

        public PatientController(IPatientService a_patientService, ITokenService a_tokenService, IPermissionService a_permission)
        {
            m_service = a_patientService;
            m_validator = new Methods.Validator(a_tokenService, a_permission);
        }

        /// <summary>
        /// Endpoint to attendance records
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpGet, Route("records/{a_start}/{a_end}")]
        public  ActionResult<AttendanceObject> Analysis(DateTime a_start, DateTime a_end)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 11);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            var attendance = m_service.GetAttendance(a_start, a_end).Result;
            return Ok(attendance);
        }
        /// <summary>
        /// Endpoint to attendance records
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpGet, Route("records/history/{a_id}")]
        public ActionResult<List<MedHistory>> History(int a_id)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 11);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            var history = m_service.GetPatientRecord(a_id).Result;
            return Ok(history);
        }
        /// <summary>
        /// Endpoint to look up a patient using their id
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpGet, Route("records/profile/{a_id}")]
        public ActionResult<List<MedHistory>> GetPatient(string a_id)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 1);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            var history = m_service.GetPatientProfile(a_id);
            return Ok(history);
        }

    }
}
