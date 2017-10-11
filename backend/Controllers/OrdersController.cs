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
    public class OrdersController : Controller
    {
        // GET api/Orders
        [HttpGet]
        public async Task<IEnumerable<Order>> Get() => await Task.Run<IEnumerable<Order>>(() =>
        {
            using (var db = new Db())
            {
                // Ensure to call ToList so that the DB doesn't dispose itself
                return db.Orders.ToList();
            }
        });

        // GET api/Orders/5
        [HttpGet("{id}")]
        public async Task<Order> Get(int id) => await Task.Run<Order>(() =>
        {
            using (var db = new Db())
            {
                return db.Orders.FirstOrDefault(s => s.Id == id);
            }
        });

        // POST api/Orders
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Order value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.Orders.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/Orders", value);
                }
            }
            else
                return BadRequest();
        });

        // PUT api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Order value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.Orders.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/Orders", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.Orders.Remove(db.Orders.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });
    }
}
