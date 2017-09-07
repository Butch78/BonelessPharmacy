using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]

    public class StaffController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<Staff>> Get() => await Task.Run<IEnumerable<Staff>>()) =>
        {
            using (var db = new Db())
            {
            // Ensure to call toList so that the DB doesn't dispose itself
            return db.Staff.toList();
            }
        });
 
    //Get api/
    [HttpGet("{}")]
    }
}