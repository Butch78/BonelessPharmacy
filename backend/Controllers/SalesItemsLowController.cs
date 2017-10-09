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
        /// Return how many stock items are low by default this value is 5.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SalesItem>> Get() => await Task.Run<IEnumerable<SalesItem>>(() =>
        {
            using (var db = new Db())
            {
                return db.SalesItems.Where(s => s.StockOnHand < 5).ToList();
            }
        });
        /// <summary>
        /// Return how many stock items are low by default this value is 5.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{amount}")]
        public async Task<IEnumerable<SalesItem>> Get(int amount = 5) => await Task.Run<IEnumerable<SalesItem>>(() =>
        {
            using (var db = new Db())
            {
                return db.SalesItems.Where(s => s.StockOnHand < amount).ToList();
            }
        });
    }
}