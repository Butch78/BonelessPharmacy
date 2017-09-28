using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A sale is a collection of sales records
    /// </summary>
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public List<SalesRecord> Contents { get; set; }

        //TODO: Implement Staff and customer Foreign Key relationship
        // public int StaffId { get; set; }

        // public int CustomerId { get; set; }

        
        /// <summary>
        /// Retrieve the valid sales from the database
        /// </summary>
        /// <param name="db">an active database context</param>
        /// <returns></returns>
        public static List<Sale> ValidSales (Db db) => db.Sales
                    .Include(s => s.Contents)
                    .ThenInclude(sr => sr.SalesItem)
                    .ThenInclude(si => si.Measurement)
                    .Where(s => s.Contents != null && s.Contents.Count > 0)
                    .ToList();
    }
}