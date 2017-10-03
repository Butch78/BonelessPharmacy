using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BonelessPharmacyBackend
{
    public class LowStockReportFactory : IReportFactory
    {
        private List<SalesItem> _stock;

        private int _lowThreshold;

        public string Type => "Low Stock";

        /// <summary>
        /// Create a new LowStockReport factory with an optional threshold
        /// </summary>
        /// <param name="lowThreshold"></param>
        public LowStockReportFactory(int lowThreshold = 5)
        {
            _lowThreshold = lowThreshold;
            PopulateLowStock();
        }

        /// <summary>
        /// Populate sales between two dates
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private async void PopulateLowStock()
        {
            using (var db = new Db())
            {
                _stock = await db.SalesItems
                    .Include(s => s.Measurement)
                    .Where(s => s.StockOnHand <= _lowThreshold)
                    .ToListAsync();
            }
        }

        public async Task<string> GenerateCsv() => await Task.Run(() =>
        {
            var writer = new StringWriter();
            var csv = new CsvWriter(writer);

            WriteHeading(csv);
            foreach (var s in _stock)
                WriteField(csv, s);

            return writer.ToString();
        });

        // <summary>
        /// Write the standard heading for a sale report
        /// </summary>
        /// <param name="csv"></param>/
        private void WriteHeading(CsvWriter csv)
        {
            csv.WriteField("ID");
            csv.WriteField("Name");
            csv.WriteField("SOH");
            csv.NextRecord();
        }

        /// <summary>
        /// Write the standard field for a sale
        /// </summary>
        /// <param name="csv">csv writer</param>
        /// <param name="sale">the sale to write</param>
        private void WriteField(CsvWriter csv, SalesItem item)
        {
            csv.WriteField(item.Id);
            csv.WriteField(item.Name);
            csv.WriteField(item.StockOnHand);
            csv.NextRecord();
        }

        public async Task<string> GenerateJson() => await Task.Run(() => JsonConvert.SerializeObject(_stock));
    }
}