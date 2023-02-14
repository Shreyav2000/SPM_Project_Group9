using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using KNUST_Medical_Refund.Shared.Models;

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SupportDocumentsController : ControllerBase
    {
        private readonly KNUSTMISContext _context;

        public SupportDocumentsController(KNUSTMISContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SupportDocument>>> GetSupportDocument(string id)
        {
            List<RefundSupportDocument> supportDocuments = await _context.RefundSupportDocuments.Where(i => i.ReportId == id).ToListAsync();

            if (supportDocuments == null)
            {
                return NotFound();
            }

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
