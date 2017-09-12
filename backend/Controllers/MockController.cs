using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{type}")]
        public async Task<object> Get(string type) => await Task.Run<object>(() => {
            switch (type.ToLower())
            {
                case "salesitem":
                    return ModelFactory.SalesItem.Generate();
                
                case "measurement":
                    return ModelFactory.Measurement.Generate();

                case "staff":
                    return ModelFactory.Staff.Generate();

                default:
                    return new {Error = "Invalid Type"};
            }
        });
    }
}
