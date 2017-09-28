using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    public class OrderItemController : Controller
    {
        // GET api/OrderItems
        [HttpGet]
        public async Task<IEnumerable<OrderItem>> Get() => await Task.Run<IEnumerable<OrderItem>>(() =>
        {
            using (var db = new Db())
            {
                // Ensure to call ToList so that the DB doesn't dispose itself
                return db.OrderItems.ToList();
            }
        });

        // GET api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<OrderItem> Get(int id) => await Task.Run<OrderItem>(() =>
        {
            using (var db = new Db())
            {
                return db.OrderItems.FirstOrDefault(s => s.Id == id);
            }
        });

        

        // POST api/OrderItems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderItem value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.OrderItems.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/OrderItems", value);
                }
            }
            else
                return BadRequest();
        });

        // PUT api/OrderItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]OrderItem value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.OrderItems.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/OrderItems", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.OrderItems.Remove(db.OrderItems.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });
    }
}
