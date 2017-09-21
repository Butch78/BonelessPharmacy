using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        // GET api/Roles
        [HttpGet]
        public async Task<IEnumerable<Role>> Get() => await Task.Run<IEnumerable<Role>>(() =>
        {
            using (var db = new Db())
            {
                // Ensure to call ToList so that the DB doesn't dispose itself
                return db.Roles.ToList();
            }
        });

        // GET api/Roles/5
        [HttpGet("{id}")]
        public async Task<Role> Get(int id) => await Task.Run<Role>(() =>
        {
            using (var db = new Db())
            {
                return db.Roles.FirstOrDefault(s => s.Id == id);
            }
        });

        // POST api/Roles
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Role value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.Roles.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/Roles", value);
                }
            }
            else
                return BadRequest();
        });

        // PUT api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Role value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.Roles.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/Roles", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.Roles.Remove(db.Roles.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });
    }
}
