using KNUST_Medical_Refund.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Authorize]
    [AllowAnonymous, Route("Pharma")]
    [ApiController]
    public class PharmacyValues : ControllerBase
    {

        private readonly KNUSTMISContext _context;

        public PharmacyValues(KNUSTMISContext context)
        {
            _context = context;
        }

        // GET: api/<PharmacyValues>
        [HttpGet, Route("getfrequency")]
        public async Task<IEnumerable<DrugPrescriptionFrequency>> Pending()
        {
            return await _context.DrugPrescriptionFrequencies.ToListAsync();
        }
        [HttpGet, Route("getUnits")]
        public async Task<IEnumerable<DrugUsageForm>> getUnits()
        {
            return await _context.DrugUsageForms.ToListAsync();
        }
        [HttpGet, Route("getRoute")]
        public async Task<IEnumerable<DrugRouteOfAdministration>> getRoute()
        {
            return await _context.DrugRouteOfAdministrations.ToListAsync();
        }
        // GET api/<PharmacyValues>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PharmacyValues>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PharmacyValues>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PharmacyValues>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
