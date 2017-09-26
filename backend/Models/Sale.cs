using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}