using KNUST_Medical_Refund.Shared;
using KNUST_Medical_Refund.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagePuller : ControllerBase
    {
        private readonly KNUSTMISContext _context;
        public ImagePuller(KNUSTMISContext context)
        {
            _context = context;
        }

        [HttpGet("{filter}", Name = "Images")]
        public IActionResult Get(string filename)
        {
            return PhysicalFile(ServerLocation.Attachments + filename, "image/jpeg");
        }
    }
}
