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
    public class SalesRecordsController : Controller
    {
        // GET api/SalesRecords
        [HttpGet]
        public async Task<IEnumerable<SalesRecord>> Get() => await Task.Run<IEnumerable<SalesRecord>>(() =>
        {
            using (var db = new Db())
            {
                // Ensure to call ToList so that the DB doesn't dispose itself
                return db.SalesRecords
                    .Include(s => s.SalesItem)
                    .Include(s => s.Sale)
                    .ToList();
            }
        });

        // GET api/SalesRecords/5
        [HttpGet("{id}")]
        public async Task<SalesRecord> Get(int id) => await Task.Run<SalesRecord>(() =>
        {
            using (var db = new Db())
            {
                return db.SalesRecords
                        .Include(s => s.SalesItem)
                        .Include(s => s.Sale)
                        .ToList()
                        .FirstOrDefault(s => s.Id == id);
            }
        });

        // POST api/SalesRecords
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SalesRecord value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.SalesRecords.AddAsync(await value.ProcessSalesRecord());
                    await updateStockOnHandAsync(db, value.ItemId, value.Quantity);
                    await db.SaveChangesAsync();
                    return Created("api/SalesRecords", value);
                }
            }
            else
                return BadRequest();
        });

        // PUT api/SalesRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]SalesRecord value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.SalesRecords.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/SalesRecords", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/SalesRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.SalesRecords.Remove(db.SalesRecords.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });

        /// <summary>
        /// Update the stock on hand for a certain salesitem
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="id">the id of the salesitem</param>
        /// <param name="amount">The amount to change</param>
        /// <returns></returns>
        private async Task updateStockOnHandAsync (Db db, int id, int amount) => await Task.Run(async () => {
            var item = await db.SalesItems.FirstOrDefaultAsync(s => s.Id == id);
            item.StockOnHand -= amount;
            db.SalesItems.Update(item);
            await db.SaveChangesAsync();
        });
    }
}
