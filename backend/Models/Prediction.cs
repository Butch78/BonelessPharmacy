using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A prediction wrapper for SalesItems in the BonelessPharmacy system
    /// </summary>
    public class Prediction
    {
        private Dictionary<string, double> _expectedSales = new Dictionary<string, double>();

        public Dictionary<string, double> ExpectedSales => _expectedSales;

        private SalesItem _item;

        public SalesItem Item => _item;

        public Prediction(SalesItem item)
        {
            _item = item;
            PopulateData();
        }

        /// <summary>
        /// Using a synchronous database connection, populate the Prediction objects data.
        /// </summary>
        private void PopulateData()
        {
            using (var db = new Db())
            {
                var pastWeekSales = Sale.ValidSales(db)
                    .Where(s => s.CreatedAt >= DateTime.Today.Subtract(new TimeSpan(7,0,0,0))).ToList();
                var pastMonthSales = Sale.ValidSales(db)
                    .Where(s => s.CreatedAt >= DateTime.Today.Subtract(new TimeSpan(30,0,0,0,0))).ToList();

                var pastWeekItems = pastWeekSales.Count > 0 ? pastWeekSales.SelectMany(s => s.Contents)
                    .Where(s => s.ItemId == _item.Id).ToList() : new List<SalesRecord>();
                var pastMonthItems = pastMonthSales.Count > 0 ? pastWeekSales.SelectMany(s => s.Contents)
                    .Where(s => s.ItemId == _item.Id).ToList() : new List<SalesRecord>();

                _expectedSales["weekly"] = pastWeekItems.Count > 0 ? Math.Floor(pastWeekItems.Average(s => s.Quantity)) : 0;
                _expectedSales["monthly"] = pastMonthItems.Count > 0 ? Math.Floor(pastMonthItems.Average(s => s.Quantity)) : 0;
            }
        }
    }
}