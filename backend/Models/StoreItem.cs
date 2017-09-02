using System;
using System.ComponentModel.DataAnnotations;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A product in the BonelessPharmacy system
    /// </summary>
    public class StoreItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}