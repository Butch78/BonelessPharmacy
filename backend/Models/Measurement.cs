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
        /// The primary key of the Measurement
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// The suffix that will be appended to the sales item
        /// when this measurement is applied
        /// </summary>
        /// <returns></returns>
        public string Suffix { get; set; }

        /// <summary>
        /// A description of the Measurement
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
    }
}