using System;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend
{
    public class Db : DbContext
    {
        public DbSet<SalesItem> SalesItems { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Main.db", x => x.SuppressForeignKeyEnforcement());
        }
    }
}