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
    public class AnalysisController : ControllerBase
    {
        private readonly Methods.Validator m_validator;
        private readonly IDrugService m_service;
        private readonly IDoctorAnalysis m_doctorAnalysis;
        private readonly ITokenService m_tokenService;
        private readonly IPatientService m_patientService;

        public AnalysisController(IPatientService a_patientService, IDrugService a_drugService, ITokenService a_tokenService, IPermissionService a_permission, IDoctorAnalysis doctorAnalysis, ITokenService tokenService)
        {
            m_service = a_drugService;
            m_validator = new Methods.Validator(a_tokenService, a_permission);
            m_doctorAnalysis = doctorAnalysis;
            m_tokenService = tokenService;
            m_patientService = a_patientService;
        }

        /// <summary>
        /// Endpoint to return analysis on actions by specified doctor within a specified period
        /// </summary>
        /// <param name="a_start"></param>
        /// <param name="a_end"></param>
        /// <param name="a_id"></param>
        [Authorize]
        [HttpGet, Route("performance/{a_start}/{a_end}")]
        public ActionResult<DoctorAnalysis> DoctorAnalysis(DateTime a_start, DateTime a_end)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            int? doctor = m_tokenService.GetUserIdFromToken(token);
            string? validationResult = m_validator.Validate(token, 11);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);
            return Ok(new DoctorAnalysis
            {
                AttendanceObjects = m_patientService.GetAttendance(a_start, a_end, doctor).Result,
                TopPrescribedDrugs = m_doctorAnalysis.TopPrescribedDrugs(a_start, a_end, doctor!.Value).Result,
                TopCases = m_doctorAnalysis.FrequentCases(a_start, a_end, doctor!.Value).Result,
                TotalCases = m_doctorAnalysis.Cases(a_start, a_end, doctor!.Value).Result,
            });
        }
    }
}
