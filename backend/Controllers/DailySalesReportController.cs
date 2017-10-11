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
    public class DailySalesReportController : Controller
    {
        // GET api/DailySalesReport
        [HttpGet]
        public async Task<bool> Get() => await Task.Run<bool>(async () =>
        {
            bool isNewDay = false;
            using (var db = new Db())
            {
                isNewDay = !db.ReportFiles.Any(r => 
                    r.CreatedAt.DayOfYear == DateTime.Today.DayOfYear && 
                    r.CreatedAt.Year == DateTime.Today.Year && 
                    r.Type == "Daily Sales Report"
                );
                if (isNewDay)
                    await (new DailySalesReportFactory()).WriteReport();
                return isNewDay;
            }
        });
    }
}
