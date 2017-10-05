using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// The main Database context for the BonelessPharmacy
    /// </summary>
    public class Db : DbContext
    {
        public DbSet<SalesItem> SalesItems { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
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
    /// Contains logic relating to the initial data of the Database
    /// </summary>
    public static class DbSeeders
    {
        /// <summary>
        /// Default measurements that should be included in the database
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
            }
        };

        /// <summary>
        /// Default Roles that should be included in the database
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
        public static void SeedDb(this IApplicationBuilder builder)
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
            }
        }
    }

}