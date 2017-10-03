using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// A record of a report being saved
    /// </summary>
    public class ReportFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string FileName { get; set; }
    }
}