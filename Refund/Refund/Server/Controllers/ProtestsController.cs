using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using KNUST_Medical_Refund.Shared.Models;

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Authorize]
    [AllowAnonymous, Route("api/[controller]")]
    [ApiController]
    public class ProtestsController : ControllerBase
    {
        private readonly KNUSTMISContext _context;

        public ProtestsController(KNUSTMISContext context)
        {
            _context = context;
        }

        // GET: api/Protests
        [Route("pending"), HttpGet()]
        public async Task<List<Request>> GetTblProtests()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];

            string token = accessToken;
            token = token.Substring(6).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            string role = jwtToken.Claims.First(claim => claim.Type == "role").Value;
            string group = jwtToken.Claims.First(claim => claim.Type == "group").Value;

            List<Request> requests = new List<Request>();

            if (group == "over")
            {
                requests = await (from protest in _context.TblProtests.AsQueryable()
                                  join request in _context.RefundRequestInfos on protest.RequestId equals request.RequestId into Requests
                                  from p in Requests.DefaultIfEmpty()
                                  join c in _context.ClaimantInfos on p.ClaimantId equals c.ClaimantId into Claimants
                                  from claim in Claimants.DefaultIfEmpty()
                                  join b in _context.RequestBeneficiaries on p.RequestId equals b.RequestId into Beneficiaries
                                  from beneficiary in Beneficiaries.DefaultIfEmpty()
                                  join pre in _context.PreprocessedDates on p.RequestId equals pre.RequestId into Preprocessed
                                  from processed in Preprocessed.DefaultIfEmpty()
                                  where
                                  //protest.DateProtested.Value.Month == month && protest.DateProtested.Value.Year == year &&
                                  protest.Open.Value && p.RequestedAmount >= 2000
                                  select new Request()
                                  {
                                      ApprovedAmount = p.ApprovedAmount.HasValue ? double.Parse(p.ApprovedAmount.Value.ToString()) : 0,
                                      Beneficiary = beneficiary,
                                      BeneficiaryType = p.BeneficiaryType,
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
                                      Vetter = _context.Users.Where(u => u.UserId.ToString() == p.VetterId).Select(u => u.FullName).FirstOrDefault(),
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
                                      Status = p.Status,
                                      protested = new Protest
                                      {
                                          attachments = _context.RefundSupportDocuments.Where(r => r.ReportId == protest.ProtestId).Select(d => new SupportDocument
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
                                          comments = protest.Comments,
                                          dateprocessed = protest.DateProcessed,
                                          dateprotested = protest.DateProtested,
                                          open = protest.Open,
                                          protestID = protest.ProtestId,
                                          requestID = protest.RequestId
                                      }
                                  }
                      ).ToListAsync();
            }
            else
            {
                requests = await (from protest in _context.TblProtests.AsQueryable()
                                  join request in _context.RefundRequestInfos on protest.RequestId equals request.RequestId into Requests
                                  from p in Requests.DefaultIfEmpty()
                                  join c in _context.ClaimantInfos on p.ClaimantId equals c.ClaimantId into Claimants
                                  from claim in Claimants.DefaultIfEmpty()
                                  join b in _context.RequestBeneficiaries on p.RequestId equals b.RequestId into Beneficiaries
                                  from beneficiary in Beneficiaries.DefaultIfEmpty()
                                  join pre in _context.PreprocessedDates on p.RequestId equals pre.RequestId into Preprocessed
                                  from processed in Preprocessed.DefaultIfEmpty()
                                  where 
                                  //protest.DateProtested.Value.Month == month && protest.DateProtested.Value.Year == year &&
                                  protest.Open.Value && p.RequestedAmount < 2000
                                  select new Request()
                                  {
                                      ApprovedAmount = p.ApprovedAmount.HasValue ? double.Parse(p.ApprovedAmount.Value.ToString()) : 0,
                                      Beneficiary = beneficiary,
                                      BeneficiaryType = p.BeneficiaryType,
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
                                      Vetter = _context.Users.Where(u => u.UserId.ToString() == p.VetterId).Select(u => u.FullName).FirstOrDefault(),
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
                                      Status = p.Status,
                                      protested = new Protest
                                      {
                                          attachments = _context.RefundSupportDocuments.Where(r => r.ReportId == protest.ProtestId).Select(d => new SupportDocument
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
                                          comments = protest.Comments,
                                          dateprocessed = protest.DateProcessed,
                                          dateprotested = protest.DateProtested,
                                          open = protest.Open,
                                          protestID = protest.ProtestId,
                                          requestID = protest.RequestId
                                      }
                                  }
                     ).ToListAsync();
            }
            return requests;
        }

        [HttpPost]
        public async Task<IActionResult> VetProtest(Protest protest)
        {
            try
            {
                TblProtest protested = await _context.TblProtests.Where(p => p.ProtestId == protest.protestID).FirstAsync();
                protested.Open = protest.open;
                protested.DateProcessed = protest.dateprocessed;
                protested.AmountApproved = decimal.Parse(protest.attachments.Sum(d => d.amount.Value).ToString());
                _context.Entry(protested).State = EntityState.Modified;

                foreach (SupportDocument document in protest.attachments)
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
                if (!ProtestExists(protest.protestID))
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

        private bool ProtestExists(string id)
        {
            return _context.TblProtests.Any(e => e.ProtestId == id);
        }
        public async Task<List<SupportDocument>> GetSupportDocument(string id)
        {
            List<RefundSupportDocument> supportDocuments = await _context.RefundSupportDocuments.Where(i => i.ReportId == id).ToListAsync();

            List<SupportDocument> supportDocument = new List<SupportDocument>();

            foreach (RefundSupportDocument document in supportDocuments)
            {
                SupportDocument support = new SupportDocument();
                support.item = document.Item;
                support.reportID = document.ReportId;
                support.documentGUID = document.DocumentGuid;
                support.documentID = document.DocumentId;
                support.documentType = await _context.DocumentTypes.Where(d => d.TypeCode == document.DocumentType).Select(n => n.TypeName).FirstOrDefaultAsync();
                support.date = document.Date.Value;
                support.companyName = document.CompanyName;
                support.clientName = document.ClientName;
                support.attachment = document.Attachment;
                support.amount = double.Parse(document.Amount.HasValue ? document.Amount.Value.ToString() : "0");
                support.parseID = document.ParseId;
                if (document.VetState.HasValue)
                    support.vetState = document.VetState.Value;
                support.vetting = await _context.VettedDocuments.Where(d => d.DocumentGuid == support.documentGUID).FirstOrDefaultAsync();

                supportDocument.Add(support);
            }

            return supportDocument;
        }
    }
}
