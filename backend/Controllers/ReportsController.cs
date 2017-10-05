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
        [HttpGet]
        public async Task<IEnumerable<ReportFile>> Get() => await Task.Run(() => {
            using (var db = new Db())
            {
                return db.ReportFiles.ToList();
            }
        });

        [HttpGet("{type}")]
        public async Task<IEnumerable<ReportFile>> Get(string type) => await Task.Run(() => 
        {
            using (var db = new Db())
            {
                return db.ReportFiles.Where(r => r.Type.ToLower() == type.ToLower()).ToList() ?? db.ReportFiles.ToList();
            }
        });
        // GET api/Reports
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Dictionary<string, string> options) => await Task.Run<IActionResult>( async() =>
        {
            IReportFactory factory;
            string type = options.ContainsKey("type") ? options["type"] : "sales";
            bool isJson = options.ContainsKey("json") ? Boolean.Parse(options["json"]) : false;
            bool willSave = options.ContainsKey("save") ? Boolean.Parse(options["save"]) : true;
            
            DateTime begin = options.ContainsKey("begin") ? 
                DateTime.Parse(options["begin"]) : DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            DateTime end = options.ContainsKey("end") ? DateTime.Parse(options["end"]) : DateTime.Now;
            

            switch (type)
            {
                case "sales":
                    factory = new SalesReportFactory(begin, end);
                    break;
                case "stock":
                    factory = new StockReportFactory(begin, end);
                    break;
                case "low":
                    factory = new LowStockReportFactory(options.ContainsKey("threshold") ? 
                        Int32.Parse(options["threshold"]) : 5);
                    break;
                default:
                    throw new Exception("Invalid Report Type");
            }
            if (willSave)
                await factory.WriteReport();
            return Content(isJson ? await factory.GenerateJson() : await factory.GenerateCsv());
        });
    }
}