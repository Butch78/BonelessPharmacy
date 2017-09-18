using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    public class MockController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Error";
        }
        // GET api/Mock/salesitem
        [HttpGet("{type}/{fill?}")]
        public async Task<object> Get(string type, int fill = 0) => await Task.Run<object>(async () =>
        {
            object result;
            bool isFilling = fill == 1;
            switch (type.ToLower())
            {
                case "salesitem":
                    result = ModelFactory.SalesItem.Generate();
                    // Foreign key Constraint
                    (result as SalesItem).MeasurementId = new Random().Next(1, 5);
                    break;

                case "measurement":
                    result = ModelFactory.Measurement.Generate();
                    break;

                case "staff":
                    result = ModelFactory.Staff.Generate();
                    break;

                default:
                    throw new Exception("Invalid Type");
            }
            if (isFilling)
            {
                using (var db = new Db())
                {
                    await db.AddAsync(result);
                    await db.SaveChangesAsync();
                }
            }
            return result;
        });
    }
}
