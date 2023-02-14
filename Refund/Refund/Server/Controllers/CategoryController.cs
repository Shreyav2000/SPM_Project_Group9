using KNUST_Medical_Refund.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Route("staffcategory")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly KNUSTMISContext _context;

        public CategoryController(KNUSTMISContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Category>> Pending()
        {
            return await _context.Categories.AsQueryable().ToListAsync();
        }

    }
}
