using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    public class ArchiveItemsController : Controller
    {
        // GET api/ArchiveItems
        [HttpGet]
        public async Task<IEnumerable<Archive>> Get() => await Task.Run<IEnumerable<Archive>>(() =>
        {
            using (var db = new Db())
            {
                // Ensure to call ToList so that the DB doesn't dispose itself
                return db.ArchiveItems.Include(s => s.Measurement).ToList();
            }
        });

        // GET api/ArchiveItems/5
        [HttpGet("{id}")]
        public async Task<Archive> Get(int id) => await Task.Run<Archive>(() =>
        {
            using (var db = new Db())
            {
                return db.ArchiveItems.Include(s => s.Measurement).FirstOrDefault(s => s.Id == id);
            }
        });

        // POST api/ArchiveItems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Archive value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.ArchiveItems.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/ArchiveItems",
                        await db.ArchiveItems.Include(s => s.Measurement).FirstAsync(s => s.Id == value.Id)
                    );
                }
            }
            else
                return BadRequest();
        });

        // PUT api/ArchiveItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Archive value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.ArchiveItems.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/ArchiveItems", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/ArchiveItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.ArchiveItems.Remove(db.ArchiveItems.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });
    }
}
