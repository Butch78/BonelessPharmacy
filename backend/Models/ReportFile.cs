using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A record of a report being saved
    /// </summary>
    public class ReportFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string FileName { get; set; }

        /// <summary>
        /// Ensure that for each ReportFile in the Database there is an associated report file in the
        /// reports directory.
        /// 
        /// If there isn't an associated report `.csv`, the ReportFile is removed from the DB.
        /// </summary>
        public static async Task ConsolidateReportFiles(Db ctx = null) => await Task.Run(() =>
        {
            using (var db = ctx ?? new Db())
            {
                db.ReportFiles.RemoveRange(db.ReportFiles.Where(
                    (r) => !System.IO.File.Exists(Path.Combine(IReportFactoryExtensions.REPORT_DIR, r.FileName))).ToList()
                );
                db.SaveChanges();
            }
        });
    }
}