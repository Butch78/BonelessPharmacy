using System;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend
{
    public class Db : DbContext
    {
        public DbSet<SalesItem> SalesItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Main.db");
        }
    }
}