using System;
using System.ComponentModel.DataAnnotations;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A product in the BonelessPharmacy system
    /// </summary>
    public class SalesItem
    {
        [Key]
        /// <summary>
        /// The primary key of the SalesItem
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// The name
        /// </summary>
        /// <example>
        /// Panadol Paracetamol
        /// </example>
        /// <returns></returns>
        public string Name { get; set; }

        
        [Required]
        /// <summary>
        /// The supplier ID number for the product
        /// </summary>
        /// <returns></returns>
        public string SupplierCode { get; set; }

        [Required]
        /// <summary>
        /// The price for the product, without any symbol and
        /// </summary>
        /// <returns></returns>
        public double Price { get; set; }
    }
}