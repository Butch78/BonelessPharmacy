using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return db.Staff.Include(s => s.Role).ToList();
            }
        });
 
    //Get api/Staff/5
    [HttpGet("{id}")]
    public async Task<Staff> Get(int id) => await Task.Run<Staff>(() =>
    {
        using (var db = new Db())
        {
            return db.Staff.Include(s => s.Role).FirstOrDefault(s => s.Id == id);
        }
    });

    //  Post api/Staff/5
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Staff value) => await Task.Run<IActionResult>(async ()   =>
    {
        if(ModelState.IsValid)
        {
            using (var db = new Db())
            {
                await db.Staff.AddAsync(value); 
                await db.SaveChangesAsync();
                return Created("api/Staff", value);
            }
        }
        else 
            return BadRequest();
    });

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]Staff value) => await Task.Run<IActionResult>(async () =>
    {
        if(ModelState.IsValid)
        {
            value.Id = id; 
            using(var db = new Db())
            {
                db.Staff.Update(value);
                await db.SaveChangesAsync();
                return Accepted("api/Staff", value);
            }
        }
        else 
            return BadRequest();
    });

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async ()   =>
    {
        using (var db = new Db())
        {
            db.Staff.Remove(db.Staff.FirstOrDefault(s => s.Id == id));
            await db.SaveChangesAsync();
            return Accepted();
        }
    });

    }
}