using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Net.Http.Headers;
using KNUST_Medical_Refund.Shared.Models;

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessedprotestsController : ControllerBase
    {
        private readonly KNUSTMISContext _context;

        public ProcessedprotestsController(KNUSTMISContext context)
        {
            _context = context;
        }

        // GET: api/Processedprotests/5
        [HttpGet("{filter}")]
        public async Task<List<Request>> GetTblProtests(int month, int year)
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
                                  where protest.DateProcessed.Value.Month == month && protest.DateProcessed.Value.Year == year &&
                                  !protest.Open.Value && p.RequestedAmount >= 2000
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
                                  where protest.DateProcessed.Value.Month == month && protest.DateProcessed.Value.Year == year &&
                                  !protest.Open.Value && p.RequestedAmount < 2000
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
    }
}
