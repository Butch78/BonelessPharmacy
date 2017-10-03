using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// Standard interface for defining report factories
    /// </summary>
    public interface IReportFactory
    {
        Task<string> GenerateCsv();

        Task<string> GenerateJson();

        string Type { get; }
    }

    public static class IReportFactoryExtensions
    {
        /// <summary>
        /// The output directory for the report writing
        /// </summary>
        private static readonly string REPORT_DIR = "Reports";
        public static async Task<ReportFile> WriteReport(this IReportFactory self)
        {
            ReportFile res;
            string fileName = $"{Guid.NewGuid()}.csv";
            string path = $"{REPORT_DIR}/{fileName}";
            System.IO.File.WriteAllText(path, await self.GenerateCsv());
            using (var db = new Db())
            {
                db.ReportFiles.Add(res = new ReportFile(){
                    CreatedAt = DateTime.Now,
                    FileName = fileName,
                    Type = self.Type
                });
                await db.SaveChangesAsync();
            }
            return res;
        }
    }
}