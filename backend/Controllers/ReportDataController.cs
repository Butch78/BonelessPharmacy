using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReportDataController : Controller
    {
        [HttpGet("{filename}")]
        public async Task<IActionResult> Get(string filename) => await Task.Run<IActionResult>(() =>
        {
            string fullPath = $"Reports/{filename}";
            if (!System.IO.File.Exists(fullPath))
                return BadRequest();
            else
                return Content(System.IO.File.ReadAllText(fullPath));
        });
    }
}