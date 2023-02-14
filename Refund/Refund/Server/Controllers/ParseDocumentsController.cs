using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using KNUST_Medical_Refund.Shared.Models;

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Authorize]
    [AllowAnonymous,Route("documentparsing")]
    [ApiController]
    public class ParseDocumentsController : ControllerBase
    {
        private readonly KNUSTMISContext _context;

        public ParseDocumentsController(KNUSTMISContext context)
        {
            _context = context;
        }

        [HttpGet("{filter}")]
        public async Task<ActionResult<IEnumerable<RefundSupportDocumentCheck>>> GetTblProcessedDocument(string requestID)
        {
            return await _context.RefundSupportDocumentChecks.Where(r => r.RequestId == requestID).ToListAsync();
        }
    }
}
