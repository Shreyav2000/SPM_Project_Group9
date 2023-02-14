using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Net.Http.Headers;
using KNUST_Medical_Refund.Shared.Models;

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Authorize]
    [AllowAnonymous, Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly KNUSTMISContext _context;

        public RequestsController(KNUSTMISContext context)
        {
            _context = context;
        }

        [Route("pending"), HttpGet()]
        public async Task<IEnumerable<Request>> GetRefundRequestInfos()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];

            string token = accessToken;
            token = token.Substring(6).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            string role = jwtToken.Claims.First(claim => claim.Type == "role").Value;
            string group = jwtToken.Claims.First(claim => claim.Type == "group").Value;
            string userid = jwtToken.Claims.First(claim => claim.Type == "userid").Value;

            List<Request> requests;
            if (group == "over")
            {
                requests = await (from p in _context.RefundRequestInfos.AsQueryable()
                                  join c in _context.ClaimantInfos on p.ClaimantId equals c.ClaimantId into Claimants
                                  from claim in Claimants.DefaultIfEmpty()
                                  join b in _context.RequestBeneficiaries on p.RequestId equals b.RequestId into Beneficiaries
                                  from beneficiary in Beneficiaries.DefaultIfEmpty()
                                  join pre in _context.PreprocessedDates on p.RequestId equals pre.RequestId into Preprocessed
                                  from processed in Preprocessed.DefaultIfEmpty()
                                  where 
                                  //p.RequestDate.Value.Month == month && p.RequestDate.Value.Year == year &&
                                  p.Status.Contains("pending") && p.RequestedAmount >= 2000
                                  orderby p.RequestDate descending
                                  select new Request()
                                  {
                                      ApprovedAmount = p.ApprovedAmount.HasValue ? double.Parse(p.ApprovedAmount.Value.ToString()) : 0,
                                      Beneficiary = beneficiary,
                                      BeneficiaryType = p.BeneficiaryType,
                                      BeneficiaryCategory = p.BeneficiaryCategory,
                                      claimant = new ClaimantInfo
                                      {
                                          Age = claim.Age.Value,
                                          Department = claim.Department,
                                          Fullname = claim.Fullname,
                                          HospitalNo = claim.HospitalNo,
                                          Knustid = claim.Knustid,
                                          Telephone = claim.Telephone
                                      },
                                      DatesAttended = _context.TblDateAttendeds.AsQueryable().Where(d => d.ReportId == p.RequestId).
                                      Select(d => d.DateAttended.Value).ToList(),
                                      Date_processed = p.ProcessingDate,
                                      HospitalAttended = p.HospitalAttended,
                                      preprocessedDate = processed.DatePreprocessed.HasValue ? processed.DatePreprocessed.Value : null,
                                      RefundItems = _context.RefundSupportDocuments.Where(r => r.ReportId == p.RequestId).Select(d => new SupportDocument
                                      {
                                          amount = d.Amount.HasValue ? double.Parse(d.Amount.Value.ToString()) : null,
                                          attachment = d.Attachment,
                                          clientName = d.ClientName,
                                          companyName = d.CompanyName,
                                          date = d.Date.Value,
                                          documentGUID = d.DocumentGuid,
                                          documentID = d.DocumentId,
                                          documentType = _context.DocumentTypes.Where(s => s.TypeCode == d.DocumentType).Select(n => n.TypeName).FirstOrDefault(),
                                          item = d.Item,
                                          parseID = d.ParseId,
                                          reportID = d.ReportId,
                                          vetState = d.VetState,
                                          vetting = _context.VettedDocuments.Where(b => b.DocumentGuid == d.DocumentGuid).FirstOrDefault()
                                      }).ToList(),
                                      Reason = p.Reason,
                                      ReportID = p.RequestId,
                                      RequestedAmount = double.Parse(p.RequestedAmount.Value.ToString()),
                                      Request_date = p.RequestDate.Value,
                                      Status = p.Status
                                  }
                         ).ToListAsync();
            }
            else
            {
                requests = await (from p in _context.RefundRequestInfos.AsQueryable()
                                  join c in _context.ClaimantInfos on p.ClaimantId equals c.ClaimantId into Claimants
                                  from claim in Claimants.DefaultIfEmpty()
                                  join b in _context.RequestBeneficiaries on p.RequestId equals b.RequestId into Beneficiaries
                                  from beneficiary in Beneficiaries.DefaultIfEmpty()
                                  join pre in _context.PreprocessedDates on p.RequestId equals pre.RequestId into Preprocessed
                                  from processed in Preprocessed.DefaultIfEmpty()
                                  where
                                  //p.RequestDate.Value.Month == month && p.RequestDate.Value.Year == year &&
                                  p.Status.Contains("pending") && p.RequestedAmount < 2000
                                  orderby p.RequestDate descending
                                  select new Request()
                                  {
                                      ApprovedAmount = p.ApprovedAmount.HasValue ? double.Parse(p.ApprovedAmount.Value.ToString()) : 0,
                                      Beneficiary = beneficiary,
                                      BeneficiaryType = p.BeneficiaryType,
                                      BeneficiaryCategory = p.BeneficiaryCategory,
                                      claimant = new ClaimantInfo
                                      {
                                          ClaimantId = claim.ClaimantId,
                                          Age = claim.Age.Value,
                                          Department = claim.Department,
                                          Fullname = claim.Fullname,
                                          HospitalNo = claim.HospitalNo,
                                          Knustid = claim.Knustid,
                                          Telephone = claim.Telephone
                                      },
                                      Vetter = p.VetterId,
                                      DatesAttended = _context.TblDateAttendeds.AsQueryable().Where(d => d.ReportId == p.RequestId).
                                      Select(d => d.DateAttended.Value).ToList(),
                                      Date_processed = p.ProcessingDate,
                                      HospitalAttended = p.HospitalAttended,
                                      preprocessedDate = processed.DatePreprocessed.HasValue ? processed.DatePreprocessed.Value : null,
                                      Reason = p.Reason,
                                      RefundItems = _context.RefundSupportDocuments.Where(r => r.ReportId == p.RequestId).Select(d => new SupportDocument
                                      {
                                          amount = d.Amount.HasValue ? double.Parse(d.Amount.Value.ToString()) : null,
                                          attachment = d.Attachment,
                                          clientName = d.ClientName,
                                          companyName = d.CompanyName,
                                          date = d.Date.Value,
                                          documentGUID = d.DocumentGuid,
                                          documentID = d.DocumentId,
                                          documentType = _context.DocumentTypes.Where(s => s.TypeCode == d.DocumentType).Select(n => n.TypeName).FirstOrDefault(),
                                          item = d.Item,
                                          parseID = d.ParseId,
                                          reportID = d.ReportId,
                                          vetState = d.VetState,
                                          vetting = _context.VettedDocuments.Where(b => b.DocumentGuid == d.DocumentGuid).FirstOrDefault()
                                      }).ToList(),
                                      ReportID = p.RequestId,
                                      RequestedAmount = double.Parse(p.RequestedAmount.Value.ToString()),
                                      Request_date = p.RequestDate.Value,
                                      Status = p.Status
                                  }
                             ).ToListAsync();
            }
            return requests;
        }

        [HttpPost]
        public async Task<IActionResult> PutRefundRequestInfo(Request requested)
        {

            try
            {
                RefundRequestInfo refundRequest = await _context.RefundRequestInfos.Where(t => t.RequestId == requested.ReportID).FirstAsync();
                refundRequest.ApprovedAmount = decimal.Parse(requested.ApprovedAmount.ToString());
                refundRequest.ProcessingDate = requested.Date_processed;
                refundRequest.VetterId = requested.Vetter;
                if (requested.RefundItems.Where(d => d.vetState == true && d.documentType.ToLower() == "receipt").Distinct().ToList().Count > 0)
                {
                    if (requested.RefundItems.Any(d => d.vetState == false && d.documentType.ToLower() == "receipt"))
                    {
                        requested.Status = "partially approved";
                    }
                    else
                    {
                        requested.Status = "approved";
                    }
                }
                else
                {
                    requested.Status = "declined";
                }
                refundRequest.Status = requested.Status;
                _context.Entry(refundRequest).State = EntityState.Modified;

                foreach (SupportDocument document in requested.RefundItems)
                {
                    RefundSupportDocument refundSupportDocument = await _context.RefundSupportDocuments.Where(g => g.DocumentGuid == document.documentGUID).FirstAsync();
                    refundSupportDocument.VetState = document.vetState;
                    _context.Entry(refundSupportDocument).State = EntityState.Modified;

                    _context.VettedDocuments.Add(document.vetting);
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefundRequestInfoExists(requested.ReportID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool RefundRequestInfoExists(string id)
        {
            return _context.RefundRequestInfos.Any(e => e.RequestId == id);
        }
    }
}
