using System;
using System.ComponentModel.DataAnnotations;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A Role of each Staff member 
    /// </summary>
    public class Role
    {
        [Key]
        /// <summary>
        /// The primary key of the Role
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// The Name of the Role the Staff member will have 
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>
        /// A description of the Role
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
    }
}