using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BonelessPharmacyBackend
{
    /// <summary>
    /// The item type for a SalesItem
    /// </summary>
    public class ItemType
    {
        [Key]
        /// <summary>
        /// The primary key of the ItemType
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
