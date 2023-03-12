using HealthCare.Shared.Enums;
using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace HealthCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly Methods.Validator m_validator;
        private readonly IDrugService m_service;

        public DrugController(IDrugService a_drugService, ITokenService a_tokenService, IPermissionService a_permission)
        {
            m_service = a_drugService;
            m_validator = new Methods.Validator(a_tokenService, a_permission);
        }
        /// <summary>
        /// Endpoint to create a new drug
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpPost, Route("create")]
        public async Task<IActionResult> NewDrug(DrugModel a_drug)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 10);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (m_service.CreateDrug(a_drug))
                return Ok("Drug created successfully");

            return BadRequest("Error occurred, please try again later");
        }
        /// <summary>
        /// endpoint to decomission a drug
        /// </summary>
        /// <param name="a_drugId"></param>
        [Authorize]
        [HttpPut, Route("decomission")]
        public async Task<IActionResult> DecomissionDrug([FromBody]string a_drugId)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 13);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (m_service.DecomissionDrug(a_drugId))
                return Ok("Drug decomissioned successfully");

            return BadRequest("Error occurred, please try again later");
        }
        /// <summary>
        /// Endpoint to reinstate a decomissioned drug
        /// </summary>
        /// <param name="a_drugId"></param>
        [Authorize]
        [HttpPut, Route("reinstate")]
        public async Task<IActionResult> ReinstateDrug([FromBody] string a_drugId)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 12);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (m_service.ReinstateDrug(a_drugId))
                return Ok("Drug reinstated successfully");

            return BadRequest("Error occurred, please try again later");
        }
        /// <summary>
        /// Endpoint to mark drug needed for refill
        /// </summary>
        /// <param name="a_drugId"></param>
        [Authorize]
        [HttpPut, Route("markrefill")]
        public async Task<IActionResult> MarkForRefillDrug([FromBody] string a_drugId)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 12);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (m_service.MarkForRefill(a_drugId))
                return Ok("Drug successfully marked for refill");

            return BadRequest("Error occurred, please try again later");
        }
        /// <summary>
        /// Endpoint to restock drugs
        /// </summary>
        /// <param name="a_item"></param>
        [Authorize]
        [HttpPut, Route("restock")]
        public async Task<IActionResult> RestockDrug(StockItem a_item)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 12);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (m_service.RestockDrug(a_item))
                return Ok("Drug successfully restocked");

            return BadRequest("Error occurred, please try again later");
        }
        /// <summary>
        /// Endpoint point to set expired drugs
        /// </summary>
        /// <param name="expiryitem"></param>
        [Authorize]
        [HttpPut, Route("setExpired")]
        public async Task<IActionResult> RestockDrug(Expiryitem expiryitem)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 12);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (m_service.MarkAsExpired(expiryitem))
                return Ok("Drug records successfully added");

            return BadRequest("Error occurred, please try again later");
        }
        /// <summary>
        /// Endpoint to get list of drugs
        /// </summary>
        [Authorize]
        [HttpGet, Route("list")]
        public ActionResult<List<Drug>> GetDrugs()
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 11);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            return Ok(m_service.GetDrugs());
        }
        /// <summary>
        /// Endpoint to update drug details
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpPut, Route("update")]
        public ActionResult<List<Drug>> UpdateDrugs(DrugModel a_drug)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 12);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            if (m_service.UpdateDrug(a_drug))
                return Ok("Drug records successfully updated");

            return BadRequest("Error occurred, please try again later");
        }
        /// <summary>
        /// Endpoint to drug analysis
        /// </summary>
        /// <param name="a_drug"></param>
        [Authorize]
        [HttpGet, Route("performance/{a_start}/{a_end}")]
        public ActionResult<DrugAnalysis> Analysis(DateTime a_start,DateTime a_end)
        {
            string token = Request.Headers[HeaderNames.Authorization]!;
            string? validationResult = m_validator.Validate(token, 11);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            return Ok(m_service.GetAnalysis(a_start, a_end));
        }
    }
}
