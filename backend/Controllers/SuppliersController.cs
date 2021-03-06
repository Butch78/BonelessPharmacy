using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]

    public class SuppliersController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<Supplier>> Get() => await Task.Run<IEnumerable<Supplier>>(() =>
        {
            using (var db = new Db())
            {
            // Ensure to call toList so that the DB doesn't dispose itself
            return db.Suppliers.ToList();
            }
        });
 
    //Get api/Supplier/5
    [HttpGet("{id}")]
    public async Task<Supplier> Get(int id) => await Task.Run<Supplier>(()    =>
    {
        using (var db = new Db())
        {
            return db.Suppliers.FirstOrDefault(s => s.Id == id);
        }
    });

    //  Post api/Supplier/5
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Supplier value) => await Task.Run<IActionResult>(async ()   =>
    {
        if(ModelState.IsValid)
        {
            using (var db = new Db())
            {
                await db.Suppliers.AddAsync(value); 
                await db.SaveChangesAsync();
                return Created("api/Supplier", value);
            }
        }
        else 
            return BadRequest();
    });

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]Supplier value) => await Task.Run<IActionResult>(async () =>
    {
        if(ModelState.IsValid)
        {
            value.Id = id; 
            using(var db = new Db())
            {
                db.Suppliers.Update(value);
                await db.SaveChangesAsync();
                return Accepted("api/Supplier", value);
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
            db.Suppliers.Remove(db.Suppliers.FirstOrDefault(s => s.Id == id));
            await db.SaveChangesAsync();
            return Accepted();
        }
    });

    }
}