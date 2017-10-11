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
    // [Authorize]
    public class SalesItemTrendController : Controller
    {
        // GET api/SalesItems
        [HttpGet("{id}")]
        public async Task<IEnumerable<int>> Get(int id) => await Task.Run<IEnumerable<int>>(() =>
        {
            using (var db = new Db())
            {
                List<int> result = new List<int>();
                for (int i = 5; i >= 0; i--)
                {
                    var sales = Sale.ValidSales(db).Where(s =>
                        s.CreatedAt.DayOfYear > DateTime.Today.Subtract(new TimeSpan(i * 30, 0, 0, 0)).DayOfYear && 
                        s.CreatedAt.DayOfYear  <= DateTime.Today.Subtract(new TimeSpan((i - 1) * 30, 0, 0, 0)).DayOfYear);
                    var quantity = sales.SelectMany(s => s.Contents).Where(sr => sr.ItemId == id).Sum(sr => sr.Quantity);
                    result.Add(quantity);
                }
                return result;
            }
        });

    }
}
