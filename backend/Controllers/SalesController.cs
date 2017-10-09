using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        // GET api/Sales
        [HttpGet]
        public async Task<IEnumerable<Sale>> Get() => await Task.Run<IEnumerable<Sale>>(() =>
        {
            using (var db = new Db())
            {
                return Sale.ValidSales(db);
            }
        });

        // GET api/Sales/5
        [HttpGet("{id}")]
        public async Task<Sale> Get(int id) => await Task.Run<Sale>(() =>
        {
            using (var db = new Db())
            {
                return Sale.ValidSales(db)
                    .FirstOrDefault(s => s.Id == id);
            }
        });

        // POST api/Sales
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Sale value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.Sales.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/Sales", value);
                }
            }
            else
                return BadRequest();
        });

        // PUT api/Sales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Sale value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.Sales.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/Sales", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.Sales.Remove(db.Sales.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });

    }
}
