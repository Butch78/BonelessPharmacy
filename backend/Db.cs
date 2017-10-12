using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// The database context used by Boneless Pharmacy.
    /// </summary>
    public class Db : DbContext
    {
        /// <summary>
        /// The DbSet for the SalesItems table.
        /// </summary>
        public DbSet<SalesItem> SalesItems { get; set; }
        /// <summary>
        /// The DbSet for the Measurements table.
        /// </summary>
        public DbSet<Measurement> Measurements { get; set; }
        /// <summary>
        /// The DbSet for the ItemTypes table.
        /// </summary>
        public DbSet<ItemType> ItemTypes { get; set; }
        /// <summary>
        /// The DbSet for the Sales table.
        /// </summary>
        public DbSet<Sale> Sales { get; set; }
        /// <summary>
        /// The DbSet for the OrderItems table.
        /// </summary>
        public DbSet<OrderItem> OrderItems { get; set; }
        /// <summary>
        /// The DbSet for the Roles table.
        /// </summary>
        public DbSet<Role> Roles { get; set; }
        /// <summary>
        /// The DbSet for the SalesRecords table.
        /// </summary>
        public DbSet<SalesRecord> SalesRecords { get; set; }
        /// <summary>
        /// The DbSet for the Staff table.
        /// </summary>
        public DbSet<Staff> Staff { get; set; }
        /// <summary>
        /// The DbSet for the Suppliers table.
        /// </summary>
        public DbSet<Supplier> Suppliers { get; set; }
        /// <summary>
        /// The DbSet for the Orders table.
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        /// <summary>
        /// The DbSet for the Customers table.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
        /// <summary>
        /// The DbSet for the ReportFiles table.
        /// </summary>
        public DbSet<ReportFile> ReportFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Main.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesRecord>()
                        .HasOne(sr => sr.Sale)
                        .WithMany(s => s.Contents)
                        .HasForeignKey(s => s.SaleId);
        }
    }

    /// <summary>
    /// Contains methods used by the `seed` function of the database context.
    /// 
    /// Use this class to include default tables and data required by the system.
    /// </summary>
    public static class DbSeeders
    {
        /// <summary>
        /// Default measurements that should be included in the database.
        /// </summary>
        /// <returns></returns>
        private static readonly List<Measurement> MEASUREMENT_DEFAULTS = new List<Measurement>()
        {
            new Measurement()
            {
                Suffix = "mg",
                Description = "Milligrams"
            },
            new Measurement()
            {
                Suffix = "g",
                Description = "Grams"
            },
            new Measurement()
            {
                Suffix = "ml",
                Description = "Milliletres"
            },
            new Measurement()
            {
                Suffix = "tablet/s",
                Description = "Tablets"
            },
            new Measurement()
            {
                Suffix = "capsule/s",
                Description = "Capsules"
            },
            new Measurement()
            {
                Suffix = "unit/s",
                Description = "Units"
            }
        };

        /// <summary>
        /// Default Roles that should be included in the database.
        /// </summary>
        /// <returns></returns>
        private static readonly List<Role> ROLE_DEFAULTS = new List<Role>()
        {
            new Role()
            {
                Name = "Manager",
                Description = "In charge of the Staff of the Store "
            },
            new Role()
            {
                Name = "Pharmacist",
                Description = "The person in charge of dispending Drugs"
            },
            new Role()
            {
                Name = "Sale Assistant",
                Description = "Person who Make the point of sales"
            }
        };

        /// <summary>
        /// The Administration user by BonelessPharmacy.
        /// </summary>
        /// <remarks>This user is not required, instead exists to allow for easier usage of the system.</remarks>
        /// <returns></returns>
        private static readonly Staff ADMIN_USER = new Staff()
        {
            Name = "Admin",
            Password = "password",
            PhoneNumber = "1234567890",
            RoleId = 1
        };


        /// <summary>
        /// Used to seed the database with its initial data set
        /// </summary>
        /// <example>
        /// app.SeedDb();
        /// </example>
        /// <param name="builder"></param>
        public static void SeedDb(this IApplicationBuilder builder, IConfiguration conf)
        {
            using (var db = new Db())
            {
                if (!db.Measurements.Any())
                {
                    Console.WriteLine("Seeding Measurements Table...");
                    db.Measurements.AddRange(MEASUREMENT_DEFAULTS);
                }

                if (!db.Roles.Any())
                {
                    Console.WriteLine("Seeding Roles Table...");
                    db.Roles.AddRange(ROLE_DEFAULTS);
                }

                db.SaveChanges();

                if (!db.Staff.Any())
                {
                    Console.WriteLine("Adding Admin User");
                    db.Staff.Add(ADMIN_USER);
                    db.SaveChanges();
                }

                if (Boolean.Parse(conf["Config:SupportOldSaleRecords"]))
                    UpdateSalePrice(db);
            }
        }
        /// <summary>
        /// Update the sale price field to be the current value set for the item.
        /// </summary>
        /// <remarks>
        /// This field method exists for backwards compatibility reasons. It shouldn't be
        /// used in newer versions of the program.
        /// </remarks>
        /// <param name="db"></param>
        private static void UpdateSalePrice(Db db)
        {
            foreach (var sR in db.SalesRecords.Include(s => s.SalesItem))
            {
                if (sR.SalePrice == 0.00)
                {
                    Console.WriteLine($"Updating SalePrice of SalesRecord #${sR.Id}");
                    sR.SalePrice = sR.SalesItem.Price;
                    db.SalesRecords.Update(sR);
                    db.SaveChanges();
                }
            }
        }
    }

}