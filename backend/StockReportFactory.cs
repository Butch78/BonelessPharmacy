using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend
{
    public class StockReportFactory : IReportFactory
    {
        private Dictionary<int, int> _sold;

        public StockReportFactory() => _sold = new Dictionary<int, int>();

        /// <summary>
        /// Create a new report factory beteween two dates
        /// </summary>
        /// <param name="begin">the start date for the report</param>
        /// <param name="end">the end date</param>
        /// <returns></returns>
        public StockReportFactory(DateTime begin, DateTime end) : this() => PopulateStock(begin, end);

        /// <summary>
        /// Populate sales between two dates
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private void PopulateStock(DateTime begin, DateTime end)
        {
            using (var db = new Db())
            {
                var sales = db.Sales
                    .Include(s => s.Contents)
                    .ThenInclude(sr => sr.SalesItem)
                    .ThenInclude(si => si.Measurement)
                    .Where(s => s.CreatedAt >= begin && s.CreatedAt <= end)
                    .SelectMany(s => s.Contents)
                    .ToList();
                foreach (var s in sales)
                {
                    if (!_sold.ContainsKey(s.Id))
                        _sold[s.Id] = 0;
                    _sold[s.Id] += s.Quantity;
                }
            }
        }

        public async Task<string> GenerateCsv() => await Task.Run(() =>
        {
            var writer = new StringWriter();
            var csv = new CsvWriter(writer);

            WriteHeading(csv);
            foreach (var s in _sold)
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
            csv.WriteField("Amount Sold");
            csv.NextRecord();
        }

        /// <summary>
        /// Write the standard field for a sale
        /// </summary>
        /// <param name="csv">csv writer</param>
        /// <param name="sale">the sale to write</param>
        private void WriteField(CsvWriter csv, KeyValuePair<int, int> sold)
        {
            csv.WriteField(sold.Key);
            csv.WriteField(sold.Value);
            csv.NextRecord();
        }
    }
}