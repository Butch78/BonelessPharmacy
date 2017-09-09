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
        public async Task<IEnumerable<Staff>> Get() => await Task.Run<IEnumerable<Staff>>(() =>
        {
            using (var db = new Db())
            {
            // Ensure to call toList so that the DB doesn't dispose itself
            return db.Staff.ToList();
            }
        });
 
    //Get api/Staff/5
    [HttpGet("{id}")]
    public async Task<Staff> Get(int id) => await Task.Run<Staff>(()    =>
    {
        using (var db = new Db())
        {
            return db.Staff.FirstOrDefault(s => s.Id == id);
        }
    });

    



    }
}