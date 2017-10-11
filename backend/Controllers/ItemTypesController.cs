using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ItemTypesController : Controller
    {
        // GET api/ItemTypes
        [HttpGet]
        public async Task<IEnumerable<ItemType>> Get() => await Task.Run<IEnumerable<ItemType>>(() =>
        {
            using (var db = new Db())
            {
                return db.ItemTypes.ToList();
            }
        });

        //GET api/ItemTypes/5
        [HttpGet("{id}")]
        public async Task<ItemType> Get(int id) => await Task.Run<ItemType>(() =>
        {
            using (var db = new Db())
            {
                return db.ItemTypes.FirstOrDefault(s => s.Id == id);
            }
        });

        //POST api/ItemTypes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ItemType value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                using (var db = new Db())
                {
                    await db.ItemTypes.AddAsync(value);
                    await db.SaveChangesAsync();
                    return Created("api/ItemTypes", value);
                }
            }
            else
                return BadRequest();
        });

        //PUT api/ItemTypes/5
        public async Task<IActionResult> Put(int id, [FromBody]ItemType value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.ItemTypes.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/ItemTypes", value);
                }
            }
            else
                return BadRequest();
        });

        // DELETE api/ItemTypes/5
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.ItemTypes.Remove(db.ItemTypes.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });
    }
}