using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A Supplier in the BonelessPharmacy system
    /// </summary>
    public class Supplier
    {
        [Key]
        /// <summary>
        /// The primary key of the Supplier
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// The name of the supplier
        /// </summary>
        /// <example>
        /// The Headache Company, Stomach ache fixer Company
        /// </example>
        /// <returns></returns>
        public string Name { get; set; }



        [Required]
        /// <summary>
        /// Supplier's ABN 
        /// </summary>
        /// <example>
        ///  Usual ABN Format..
        /// </example>
        /// <returns></returns>
        public String ABN { get; set;}


        [Required]
        /// <summary>
        /// Supplier's Address 
        /// </summary>
        /// <example>
        ///  P. Sherman, 42 Wallaby Way, Sydney
        /// </example>
        /// <remarks>
        /// Could include other information for more in-depth record keeping
        /// </remarks>
        /// <returns></returns>
        public string Address {get; set;}

        
        [Required]
        /// <summary>
        /// The Supplier's email address
        /// </summary>
        /// <returns></returns>
        public String Email{ get; set; }


        [Required]
        /// <summary>
        /// Supplier's Phone Number 
        /// </summary>
        /// <example>
        /// 0413554345
        /// </example>
        /// <remarks>
        /// Could include other information for more in-depth record keeping
        /// </remarks>
        /// <returns></returns>
        public String PhoneNumber { get; set; }
    }
}