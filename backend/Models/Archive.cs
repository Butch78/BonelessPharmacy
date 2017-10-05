using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A product in the BonelessPharmacy system
    /// </summary>
    public class Archive
    {
        [Key]
        /// <summary>
        /// The primary key of the Archive
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

        /// <summary>
        /// The supplier ID number for the product
        /// </summary>
        /// <remarks>
        /// This may get overriden with each order if the supplier changes
        /// </remarks>
        /// <returns></returns>
        public string SupplierCode { get; set; }

        [Required]
        /// <summary>
        /// The price for the product, GST excluded
        /// </summary>
        /// <example>
        /// 23.60
        /// </example>
        /// <returns></returns>
        public double Price { get; set; }

        [Required]
        /// <summary>
        /// The stock of this item in store
        /// </summary>
        /// <example>
        /// 16
        /// </example>
        /// <returns></returns>
        public int StockOnHand { get; set; }

        [Required]
        /// <summary>
        /// How much individual parts of the item are in a single sale of it.!--
        /// This can be a measurement of count, volume, size etc...
        /// </summary>
        /// <example>
        /// 10
        /// </example>
        /// <remarks>
        /// Maybe use "Size" instead?
        /// </remarks>
        /// <returns></returns>
        public int Amount { get; set; }

        [Required]
        /// <summary>
        /// The ID of the sale items measurement
        /// </summary>
        /// <returns></returns>
        public int MeasurementId { get; set; }

        [ForeignKey("MeasurementId")]
        /// <summary>
        /// The related measurement object
        /// </summary>
        /// <returns></returns>
        public virtual Measurement Measurement { get; set; }
    }
}