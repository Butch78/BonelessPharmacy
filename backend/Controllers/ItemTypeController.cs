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
    }
}