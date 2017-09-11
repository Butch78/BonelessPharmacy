using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        // GET api/Customers
        [HttpGet]
        {
            using (var db = new Db())
            {
                // Ensure to call ToList so that the DB doesn't dispose itself
                return db.Customers.ToList();
            }
        });

        // GET api/Customers/5
        [HttpGet("{id}")]
        {
            using (var db = new Db())
            {
                return db.Customers.FirstOrDefault(s => s.Id == id);
            }
        });

        // POST api/Customers
        [HttpPost]
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.Customers.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/Customers", value);
                }
            }
            else
                return BadRequest();
        });

        // PUT api/Customers/5
        [HttpPut("{id}")]
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.Customers.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/Customers", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.Customers.Remove(db.Customers.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });
    }
}
