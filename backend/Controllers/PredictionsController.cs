using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PredictionsController : Controller
    {
        // GET api/Predictions/5
        [HttpGet("{id}")]
        public async Task<Prediction> Get(int id) => await Task.Run<Prediction>(() =>
        {
            SalesItem item;
            using (var db = new Db())
            {
                item = db.SalesItems.FirstOrDefault(s => s.Id == id);
            }
            return new Prediction(item);
        });
    }
}
