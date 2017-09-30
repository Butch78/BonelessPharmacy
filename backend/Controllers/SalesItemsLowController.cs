using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    public class SalesItemsLowController : Controller
    {
        /// <summary>
        /// Return how many stock items are low (for now, we'll say this is a quantity of less than 5)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SalesItem>> Get() => await Task.Run<IEnumerable<SalesItem>>(() =>
        {
            var items = new List<SalesItem>();
            using (var db = new Db())
            {
                foreach (SalesItem item in db.SalesItems)
                {
                    if (item.StockOnHand < 5)
                        items.Add(item);
                }
            }
            return items;
        });
    }
}