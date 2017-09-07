using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A recording of an item which has been included in a sale
    /// </summary>
    public class SalesRecord
    {
        [Key]
        /// <summary>
        /// The primary key of the SalesRecord
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }
        
        [Required]
        /// <summary>
        /// The foreign key of the related Sale
        /// </summary>
        /// <returns></returns>
        public int SaleId { get; set; }

        [ForeignKey("SaleId")]
        /// <summary>
        /// The foreign object for the Sale
        /// </summary>
        /// <returns></returns>
        public Sale Sale { get; set; }

        [Required]
        /// <summary>
        /// The foreign key of the related SalesItem
        /// </summary>
        /// <returns></returns>
        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        /// <summary>
        /// The foreign object of the sales item
        /// </summary>
        /// <returns></returns>
        public SalesItem SalesItem { get; set; }

        [Required]
        /// <summary>
        /// The amount of the sale item purchased
        /// </summary>
        /// <returns></returns>
        public int Quantity { get; set; }
    }
}