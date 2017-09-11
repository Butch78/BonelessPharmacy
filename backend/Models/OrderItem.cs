using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A Order of items in the BonelessPharmacy system
    /// </summary>
    public class OrderItem
    {
        [Key]
        /// <summary>
        /// The primary key of the OrderItem
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        ///[Required]
        /// <summary>
        /// The foreign key of the related order 
        /// </summary>
        /// <returns></returns>
        ///public int OrderId { get; set; }

        ///[ForeignKey("OrderId")]
        /// <summary>
        /// The related order object
        /// </summary>
        /// <returns></returns>
        ///public virtual Order Order { get; set; }

        ///[Required]
        /// <summary>
        /// The foreign key of the related Supplier
        /// </summary>
        /// <returns></returns>
        ///public int SupplierCodeID {get; set; }
        
        ///[ForeignKey("SupplierCodeID")]
        /// <summary>
        /// The related Supplier object
        /// </summary>
        /// <returns></returns>
        ///public virtual Supplier Supplier { get; set; }

        [Required]
        /// <summary>
        /// The quantity of the Order
        /// </summary>
        /// <returns></returns>
        public int Quantity { get; set; }

        [Required]
        /// <summary>
        /// The price for the order, GST excluded
        /// </summary>
        /// <example>
        /// 420.60
        /// </example>
        /// <returns></returns>
        public double Price { get; set; }
    }
}
