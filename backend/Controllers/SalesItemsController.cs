using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    public class SalesItemsController : Controller
    {
        // GET api/SalesItems
        [HttpGet]
        public async Task<IEnumerable<SalesItem>> Get() => await Task.Run<IEnumerable<SalesItem>>(() => {
            using (var db = new Db())
            {
                // Ensure to call ToList so that the DB doesn't dispose itself
                return db.SalesItems.ToList();
            }
        });

        // GET api/SalesItems/5
        [HttpGet("{id}")]
        public async Task<SalesItem> Get(int id) => await Task.Run<SalesItem>(() => {
           using (var db = new Db())
           {
               return db.SalesItems.FirstOrDefault(s => s.Id == id);
           } 
        });

        // POST api/SalesItems
        [HttpPost]
        public void Post([FromBody]SalesItem value)
        {
            
        }

        // PUT api/SalesItems/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/SalesItems/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
