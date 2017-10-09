using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A sales report factory dedicated to creating reports for the last 24 hours.
    /// </summary>
    public class DailySalesReportFactory : SalesReportFactory
    {
        public DailySalesReportFactory(): base(DateTime.Now.Subtract(new TimeSpan(1,0,0,0)), DateTime.Now.Subtract(new TimeSpan(1,0,0,0))
        {
        }

        public override string Type => "Daily Sales Report";
    }
}