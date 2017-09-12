using System;
using System.ComponentModel.DataAnnotations;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A Summary of Each Customers member 
    /// </summary>
    public class Customer
    {
        [Key]
        /// <summary>
        /// The primary key of the Customers
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// The Name of the Customers  
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>
        /// The Customer's Phone Number
        /// </summary>
        /// <returns></returns>
        public string PhoneNumber { get; set; }
    }
}