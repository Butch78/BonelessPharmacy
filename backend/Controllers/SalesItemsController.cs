using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SalesItemsController : Controller
    {
        // GET api/SalesItems
        [HttpGet]
        public async Task<IEnumerable<SalesItem>> Get() => await Task.Run<IEnumerable<SalesItem>>(() =>
        {
            using (var db = new Db())
            {
                // Ensure to call ToList so that the DB doesn't dispose itself
                return db.SalesItems.Include(s => s.Measurement).Where(s => s.IsArchived == 0).ToList();
            }
        });

        // GET api/SalesItems/5
        [HttpGet("{id}")]
        public async Task<SalesItem> Get(int id) => await Task.Run<SalesItem>(() =>
        {
            using (var db = new Db())
            {
                return db.SalesItems.Include(s => s.Measurement).FirstOrDefault(s => s.Id == id);
            }
        });

        // GET api/SalesItems/Archived/
        [Route("Archived")]
        [HttpGet]
        public async Task<IEnumerable<SalesItem>> GetArchived() => await Task.Run<IEnumerable<SalesItem>>(() =>
        {
            using (var db = new Db())
            {
                return db.SalesItems.Include(s => s.Measurement).Where(s => s.IsArchived == 1).ToList();
            }
        });

        // POST api/SalesItems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SalesItem value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.SalesItems.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/SalesItems",
                        await db.SalesItems.Include(s => s.Measurement).FirstAsync(s => s.Id == value.Id)
                    );
                }
            }
            else
                return BadRequest();
        });

        // PUT api/SalesItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]SalesItem value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.SalesItems.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/SalesItems", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/SalesItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.SalesItems.Remove(db.SalesItems.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });
    }
}
