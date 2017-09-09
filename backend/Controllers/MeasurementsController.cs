using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class MeasurementsController : Controller
    {
        // GET api/Measurements
        [HttpGet]
        public async Task<IEnumerable<Measurement>> Get() => await Task.Run<IEnumerable<Measurement>>(() =>
        {
            using (var db = new Db())
            {
                // Ensure to call ToList so that the DB doesn't dispose itself
                return db.Measurements.ToList();
            }
        });

        // GET api/Measurements/5
        [HttpGet("{id}")]
        public async Task<Measurement> Get(int id) => await Task.Run<Measurement>(() =>
        {
            using (var db = new Db())
            {
                return db.Measurements.FirstOrDefault(s => s.Id == id);
            }
        });

        // POST api/Measurements
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Measurement value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.Measurements.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/Measurements", value);
                }
            }
            else
                return BadRequest();
        });

        // PUT api/Measurements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Measurement value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.Measurements.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/Measurements", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/Measurements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.Measurements.Remove(db.Measurements.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });
    }
}
