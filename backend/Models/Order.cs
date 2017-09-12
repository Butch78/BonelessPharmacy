using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// An order placed which contains order items
    /// </summary>
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public enum Status {
            In progress,
            Complete
        }
        
        [Required]
        public Status Status { get; set; }

        //TODO: Implement Staff and Supplier Foreign Key relationship
        // public int StaffId { get; set; }

        // public int SupplierId { get; set; }
    }
}