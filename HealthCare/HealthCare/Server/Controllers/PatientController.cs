﻿using HealthCare.Server.Methods;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Drawing.Drawing2D;

namespace HealthCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly Methods.Validator m_validator;
        private readonly IPatientService m_service;
        private readonly ITokenService m_tokenService;

        public PatientController(IPatientService a_patientService, ITokenService a_tokenService, IPermissionService a_permission)
        {
            m_service = a_patientService;
            m_tokenService = a_tokenService;
            m_validator = new Methods.Validator(m_tokenService, a_permission);
        }

        /// <summary>
        /// Endpoint to attendance records
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpGet, Route("records/{a_start}/{a_end}")]
        public ActionResult<List<AttendanceObject>> Analysis(DateTime a_start, DateTime a_end)
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
        /// <param name="a_id"></param>
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
        /// <param name="a_id"></param>
        [Authorize]
        [HttpGet, Route("records/profile/{a_id}")]
        public ActionResult<List<MedHistory>> GetPatient(string a_id)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 1);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            var patient = m_service.GetPatientProfile(a_id);
            return Ok(patient);
        }

        /// <summary>
        /// Endpoint to get patient consultations
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpGet, Route("records/patientConsults")]

        public ActionResult<List<UserConsults>> GetUserConsultations()
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 1);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);
            var history = m_service.GetUserConsultations();
            return Ok(history);
        }

        /// <summary>
        /// Endpoint to look up a patient using their id
        /// </summary>
        /// <param name="a_id"></param>
        [Authorize]
        [HttpGet, Route("records/profile/list/{a_id}")]
        public async Task<ActionResult<List<Patient>>> GetPatients(string a_id)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 1);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            var patients = await m_service.GetPatients(a_id);
            return Ok(patients);
        }
    }
}
