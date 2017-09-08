using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controller
{
    [Route("api/[controller]")]
    public class ItemTypeController: Controller
    {
        // GET api/ItemType
        [HttpGet]
        public async Task<IEnumerable<ItemType>> Get() => await Task.Run<IEnumerable<SalesItem>>(() =>
        {
            using (var db = new Db())
            {
                return db.ItemType.ToList();
            }
        });
        
        //GET api/ItemType
        [HttpGet("{id}")]
        public async Task<ItemType> Get(int id) => await Task.Run<ItemType>(() =>
        {
            using (var db = new Db())
            {
                return db.ItemType.FirstOrDefault(s => s.Id == id);
            }
        });

        //POST api/ItemType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ItemType value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.ItemType.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/ItemType", value);
                }
            }
            else
                return BadRequest();
        });

        //PUT api/ItemType
        public async Task<IActionResult> Put(int id, [FromBody]ItemType value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.ItemType.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/ItemType", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/ItemType
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.ItemType.Remove(db.ItemType.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });
    }
}