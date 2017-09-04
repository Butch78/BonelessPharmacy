using System;
using System.ComponentModel.DataAnnotations;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A measurement used by SalesItem to normalize the process
    /// of defining how a medicine is contained and measured.
    /// </summary>
    public class Measurement
    {
        [Key]
        /// <summary>
        /// The primary key of the SalesItem
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        [Required]
        public string Suffix { get; set; }

        public string Description { get; set; }
    }
}