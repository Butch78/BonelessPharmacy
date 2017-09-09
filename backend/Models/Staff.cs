using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A product in the BonelessPharmacy system
    /// </summary>
    public class Staff
    {
        [Key]
        /// <summary>
        /// The primary key of the Staff
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// The name
        /// </summary>
        /// <example>
        /// Matthew Corey
        /// </example>
        /// <returns></returns>
        public string Name { get; set; }

        [Required]

        /// <summary>
        /// The Role of the Employee 
        /// </summary>
        /// <remarks>
        /// This could potentially be an enum 
        /// </remarks>
        /// <returns></returns>
        public string Role { get; set; }


        [Required]
        /// <summary>
        /// Staff's Phone Number 
        /// </summary>
        /// <example>
        /// 0413554345
        /// </example>
        /// <remarks>
        /// Could include other information for more in-depth record keeping
        /// </remarks>
        /// <returns></returns>
        public String PhoneNumber { get; set; }

        [Required]
        /// <summary>
        /// The Staff member's Password to login into the system
        /// </summary>
        /// <returns></returns>
        public String Password{ get; set; }

    }
}