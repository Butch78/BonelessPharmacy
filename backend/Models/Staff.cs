using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// The name
        /// </summary>
        /// <example>
        /// Matthew Corey
        /// </example>
        public string Name { get; set; }

        /// <summary>
        /// The ID of the related role
        /// </summary>
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        /// <summary>
        /// The Role of the Employee 
        /// </summary>
        /// <remarks>
        /// Enum for different Roles 
        /// </remarks>
        public Role Role { get; set; }


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
        public String PhoneNumber { get; set; }

        [Required]
        /// <summary>
        /// The Staff member's Password to login into the system
        /// </summary>
        public String Password { get; set; }

        [JsonIgnore]
        /// <summary>
        /// Return a safe, password-less representation of the staff member
        /// </summary>
        /// <param name="self"></param>
        public Staff Safe
        {
            get
            {
                Password = null;
                return this;
            }
        }

    }
}