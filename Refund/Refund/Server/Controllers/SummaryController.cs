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
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly KNUSTMISContext _context;

        public SummaryController(KNUSTMISContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Summary>> GetRefundRequestInfo(int month, int year)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];

            string token = accessToken;
            token = token.Substring(6).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            string role = jwtToken.Claims.First(claim => claim.Type == "role").Value;
            string group = jwtToken.Claims.First(claim => claim.Type == "group").Value;
            string userid = jwtToken.Claims.First(claim => claim.Type == "userid").Value;

            List<string> requestLst = new List<string>();
            List<RefundRequestInfo> approved = new List<RefundRequestInfo>();
            Summary summary = new Summary();
            summary.approved = new List<DataItem>();
            summary.declined = new List<DataItem>();
            summary.topRefundeds = new List<TopRefunded>();
            summary.common = new List<CommonDoc>();
            if (role == "head")
            {

                approved = await _context.RefundRequestInfos.Where(r => r.ProcessingDate.Value.Month == month && r.ProcessingDate.Value.Year == year).ToListAsync();

                summary.approvedDocuments = await (from rd in _context.RefundSupportDocuments.AsQueryable()
                                                   join r in _context.RefundRequestInfos on rd.ReportId equals r.RequestId into refundapproved
                                                   from ra in refundapproved.DefaultIfEmpty()
                                                   join p in _context.TblProtests on rd.ReportId equals p.ProtestId into protestapproved
                                                   from pa in protestapproved.DefaultIfEmpty()
                                                   where ((ra.ProcessingDate.Value.Month == month && ra.ProcessingDate.Value.Year == year) ||
                                                   (pa.DateProcessed.Value.Month == month && pa.DateProcessed.Value.Year == year)) && rd.VetState.Value == true
                                                   select rd
                                                   ).CountAsync();
                summary.declinedDocuments = await (from rd in _context.RefundSupportDocuments.AsQueryable()
                                                   join r in _context.RefundRequestInfos on rd.ReportId equals r.RequestId into refundapproved
                                                   from ra in refundapproved.DefaultIfEmpty()
                                                   join p in _context.TblProtests on rd.ReportId equals p.ProtestId into protestapproved
                                                   from pa in protestapproved.DefaultIfEmpty()
                                                   where ((ra.ProcessingDate.Value.Month == month && ra.ProcessingDate.Value.Year == year) ||
                                                   (pa.DateProcessed.Value.Month == month && pa.DateProcessed.Value.Year == year)) && rd.VetState.Value == false
                                                   select rd
                                                   ).CountAsync();
                summary.protests = _context.TblProtests.Where(r => r.DateProtested.Value.Month == month && r.DateProtested.Value.Year == year).Count();


                double sumRequests = double.Parse(_context.RefundRequestInfos.Where(ap => ap.ProcessingDate.Value.Month == month &&
                ap.ProcessingDate.Value.Year == year && ap.Status != "pending").Sum(rp => rp.ApprovedAmount).Value.ToString());
                double sumProtests = double.Parse(_context.TblProtests.Where(r => r.DateProcessed.Value.Month == month && r.DateProcessed.Value.Year == year &&
               r.Open == false).Sum(rp => rp.AmountApproved).Value.ToString());
                summary.totalApproved = sumRequests + sumProtests;
            }
            else
            {

                approved = await _context.RefundRequestInfos.Where(r => r.ProcessingDate.Value.Month == month && r.ProcessingDate.Value.Year == year && r.VetterId == userid).ToListAsync();
                summary.approvedDocuments = await (from rd in _context.RefundSupportDocuments.AsQueryable()
                                                   join r in _context.RefundRequestInfos on rd.ReportId equals r.RequestId into refundapproved
                                                   from ra in refundapproved.DefaultIfEmpty()
                                                   where ra.ProcessingDate.Value.Month == month && ra.ProcessingDate.Value.Year == year
                                                   && rd.VetState.Value == true
                                                   select rd
                                                    ).CountAsync();
                summary.declinedDocuments = await (from rd in _context.RefundSupportDocuments.AsQueryable()
                                                   join r in _context.RefundRequestInfos on rd.ReportId equals r.RequestId into refundapproved
                                                   from ra in refundapproved.DefaultIfEmpty()
                                                   where ra.ProcessingDate.Value.Month == month && ra.ProcessingDate.Value.Year == year &&
                                                   rd.VetState.Value == false
                                                   select rd
                                                   ).CountAsync();
                summary.protests = _context.TblProtests.Where(r => r.DateProtested.Value.Month == month && r.DateProtested.Value.Year == year && requestLst.Contains(r.RequestId)).Count();


                double sumRequests = double.Parse(_context.RefundRequestInfos.Where(ap => ap.ProcessingDate.Value.Month == month &&
                ap.ProcessingDate.Value.Year == year && ap.Status != "pending").Sum(rp => rp.ApprovedAmount).Value.ToString());
                summary.totalApproved = sumRequests;
            }


            List<DateTime> pop = new List<DateTime>();
            List<DateTime> times = new List<DateTime>();
            foreach (var p in approved)
            {
                if (!pop.Contains(p.ProcessingDate.Value.Date))
                {
                    pop.Add(p.ProcessingDate.Value.Date);
                    DataItem dataItem = new DataItem();
                    dataItem.Date = p.ProcessingDate.Value.Date;
                    dataItem.Revenue = double.Parse(approved.Where(r => r.ProcessingDate.Value.Date == p.ProcessingDate.Value.Date).Sum(s => s.ApprovedAmount.Value).ToString());
                    summary.approved.Add(dataItem);
                }

                if (!times.Contains(p.ProcessingDate.Value.Date))
                {
                    times.Add(p.ProcessingDate.Value.Date);
                    DataItem dataItem = new DataItem();
                    dataItem.Date = p.ProcessingDate.Value.Date;
                    dataItem.Revenue = double.Parse(approved.Where(r => r.ProcessingDate.Value.Date == p.ProcessingDate.Value.Date).Sum(s => s.RequestedAmount.Value).ToString()) - double.Parse(approved.Where(r => r.ProcessingDate.Value.Date == p.ProcessingDate.Value.Date).Sum(s => s.ApprovedAmount.Value).ToString());
                    summary.declined.Add(dataItem);
                }
            }

            double totalItems = await (from rd in _context.RefundSupportDocuments.AsQueryable()
                                       join r in _context.RefundRequestInfos on rd.ReportId equals r.RequestId into refundapproved
                                       from ra in refundapproved.DefaultIfEmpty()
                                       where ra.ProcessingDate.Value.Month == month && ra.ProcessingDate.Value.Year == year
                                       && rd.VetState.Value == true
                                       select rd
                                                    ).CountAsync();
            var total = await (from transcribe in _context.PharmacyTranscribes.AsQueryable()
                               join parse in _context.PharmacyParses on transcribe.TransId equals parse.TransId into parseTable
                               from parsedItem in parseTable.DefaultIfEmpty()
                               join request in _context.RefundRequestInfos on parsedItem.RequestId equals request.RequestId
                               join documents in _context.RefundSupportDocuments on transcribe.DocumentGUID equals documents.DocumentGuid
                               where request.Status.ToLower() != "declined" && documents.Item.ToLower() == "drugs" && request.ProcessingDate.Value.Month == month
                               && request.ProcessingDate.Value.Year == year
                               select transcribe.Drugname).CountAsync();

            var dodo = await (from transcribe in _context.PharmacyTranscribes.AsQueryable()
                              join parse in _context.PharmacyParses on transcribe.TransId equals parse.TransId into parseTable
                              from parsedItem in parseTable.DefaultIfEmpty()
                              join request in _context.RefundRequestInfos on parsedItem.RequestId equals request.RequestId
                              join documents in _context.RefundSupportDocuments on transcribe.DocumentGUID equals documents.DocumentGuid
                              where request.Status.ToLower() != "declined" && documents.Item.ToLower() == "drugs" && request.ProcessingDate.Value.Month == month
                              && request.ProcessingDate.Value.Year == year
                              group transcribe by transcribe.Drugname into results
                              select new TopRefunded
                              {
                                  item = results.Key,
                                  rate = results.Count() 
                              }
                              ).Take(5).OrderByDescending(r => r.rate).ToListAsync();
            summary.topRefundeds.AddRange(dodo);
            summary.topRefundeds.ForEach(d => {
                d.rate = decimal.Parse(((d.rate / total) * 100).ToString("0.0"));
                });
            var instances = await _context.VettedDocuments.Where(ss => approved.Select(r => r.RequestId).Contains(ss.RequestId)).ToListAsync();

            List<string> typelist = new List<string>();

            //foreach (RefundSupportDocument document in dodo)
            //{
            //    if (!itemsList.Contains(document.Item.ToLower()))
            //    {
            //        itemsList.Add(document.Item.ToLower());
            //        TopRefunded top = new TopRefunded();
            //        top.item = document.Item.ToUpper();
            //        double itemFQ = dodo.Where(d => d.Item.ToLower() == document.Item.ToLower()).Count();
            //        decimal answer = decimal.Parse(((itemFQ / totalItems) * 100).ToString());
            //        top.rate = decimal.Round(answer, 2);

            //        summary.topRefundeds.Add(top);
            //    }
            //}

            foreach (VettedDocument document in instances)
            {
                if (!typelist.Contains(document.DocumentType.ToLower()))
                {
                    typelist.Add(document.DocumentType.ToLower());
                    CommonDoc top = new CommonDoc();
                    top.document = await _context.DocumentTypes.Where(tt => tt.TypeCode == document.DocumentType).FirstOrDefaultAsync();
                    top.number = instances.Where(d => d.DocumentType.ToLower() == document.DocumentType.ToLower()).Count();

                    summary.common.Add(top);
                }
            }

            return summary;
        }
    }
}
