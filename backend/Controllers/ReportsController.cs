using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        // GET api/Reports
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Dictionary<string, string> options) => await Task.Run<IActionResult>( async() =>
        {
            IReportFactory factory;
            string type = options.ContainsKey("type") ? options["type"] : "sales";
            bool isJson = options.ContainsKey("json") ? Boolean.Parse(options["json"]) : false;
            
            DateTime begin = options.ContainsKey("begin") ? 
                DateTime.Parse(options["begin"]) : DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            DateTime end = options.ContainsKey("end") ? DateTime.Parse(options["begin"]) : DateTime.Now;
            

            switch (type)
            {
                case "sales":
                    factory = new SalesReportFactory(begin, end);
                    break;
                case "stock":
                    factory = new StockReportFactory(begin, end);
                    break;
                default:
                    throw new Exception("Invalid Report Type");
            }

            return Content(isJson ? await factory.GenerateJson() : await factory.GenerateCsv());
        });
    }
}