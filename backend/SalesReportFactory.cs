using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend
{
    public class SalesReportFactory : IReportFactory
    {
        private List<Sale> _sales;

        public SalesReportFactory() => _sales = new List<Sale>();

        /// <summary>
        /// Create a new report factory using a set of sales
        /// </summary>
        /// <param name="sales"></param>
        public SalesReportFactory(List<Sale> sales) : this() => _sales = sales;

        /// <summary>
        /// Create a new report factory beteween two dates
        /// </summary>
        /// <param name="begin">the start date for the report</param>
        /// <param name="end">the end date</param>
        /// <returns></returns>
        public SalesReportFactory(DateTime begin, DateTime end) : this() => PopulateSales(begin, end);

        /// <summary>
        /// Populate sales between two dates
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private async void PopulateSalesAsync(DateTime begin, DateTime end)
        {
            await Task.Run(() =>
            {
                using (var db = new Db())
                {
                    _sales = db.Sales.Where(s => s.CreatedAt >= begin && s.CreatedAt <= end).ToList();
                }
            });
        }

        /// <summary>
        /// Populate sales between two dates
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private void PopulateSales(DateTime begin, DateTime end)
        {
            using (var db = new Db())
            {
                _sales = db.Sales
                    .Include(s => s.Contents)
                    .ThenInclude(sr => sr.SalesItem)
                    .ThenInclude(si => si.Measurement)
                    .Where(s => s.CreatedAt >= begin && s.CreatedAt <= end).ToList();
            }
        }

        public async Task<string> GenerateCsv() => await Task.Run(() =>
        {
            var writer = new StringWriter();
            var csv = new CsvWriter(writer);

            WriteHeading(csv);
            foreach (var s in _sales)
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
            csv.WriteField("DateTime");
            csv.WriteField("Total");
            csv.NextRecord();
        }

        /// <summary>
        /// Write the standard field for a sale
        /// </summary>
        /// <param name="csv">csv writer</param>
        /// <param name="sale">the sale to write</param>
        private void WriteField(CsvWriter csv, Sale sale)
        {
            csv.WriteField(sale.Id);
            csv.WriteField(sale.CreatedAt);
            csv.WriteField(sale.Contents.Select(sr => sr.Quantity * sr.SalesItem.Price).Sum());
            csv.NextRecord();
        }
    }
}