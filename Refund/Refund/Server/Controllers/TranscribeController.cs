using KNUST_Medical_Refund.Server;
using KNUST_Medical_Refund.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Authorize]
    [AllowAnonymous, Route("Transcribe")]
    [ApiController]
    public class TranscribeController : ControllerBase
    {
        private readonly KNUSTMISContext _context;

        public TranscribeController(KNUSTMISContext context)
        {
            _context = context;
        }
        [HttpGet("filter")]
        public async Task<IEnumerable<PharmacyTranscribe>> Approved(string documentGUID)
        {
            return await _context.PharmacyTranscribes.AsQueryable().Where(d => d.DocumentGUID == documentGUID).ToListAsync();
        }
    }
}
