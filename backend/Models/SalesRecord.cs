using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A recording of an item which has been included in a sale.
    /// </summary>
    public class SalesRecord
    {
        [Key]
        /// <summary>
        /// The primary key of the SalesRecord.
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }
        
        [Required]
        /// <summary>
        /// The foreign key of the related Sale.
        /// </summary>
        /// <returns></returns>
        public int SaleId { get; set; }

        [ForeignKey("SaleId")]
        [JsonIgnore]
        /// <summary>
        /// The foreign object for the Sale.
        /// </summary>
        /// <returns></returns>
        public Sale Sale { get; set; }

        [Required]
        /// <summary>
        /// The foreign key of the related SalesItem.
        /// </summary>
        /// <returns></returns>
        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        /// <summary>
        /// The foreign object of the sales item.
        /// </summary>
        /// <returns></returns>
        public SalesItem SalesItem { get; set; }

        [Required]
        /// <summary>
        /// The amount of the sale item purchased.
        /// </summary>
        /// <returns></returns>
        public int Quantity { get; set; }

        /// <summary>
        /// A reference to a `SalesItem`'s cost at the time of the sale
        /// being made.
        /// </summary>
        /// <returns></returns>
        public double SalePrice { get; set; }
    }

    public static class SalesRecordExtensionMethods
    {
        /// <summary>
        /// Automatically fill the necessary fields of a SalesRecord.
        /// </summary>
        /// <remarks>
        /// This method exists to overcome the problem Corey Jenkins found
        /// with updating the price of an existing sale price. The idea is that 
        /// saving the SalePrice to a sales record will allow us to easily keep a record
        /// of the price of an item previously.
        /// </remarks>
        public static async Task<SalesRecord> ProcessSalesRecord(this SalesRecord self) => await Task.Run(() =>
        {
            using (var db = new Db())
            {
                var salesItem = db.SalesItems.FirstOrDefault(s => s.Id == self.ItemId);
                if (salesItem != null)
                    self.SalePrice = salesItem.Price;
                return self;
            }
        });
    }
}