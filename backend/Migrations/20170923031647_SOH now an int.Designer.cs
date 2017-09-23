﻿// <auto-generated />
using BonelessPharmacyBackend;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BonelessPharmacyBackend.Migrations
{
    [DbContext(typeof(Db))]
    [Migration("20170923031647_SOH now an int")]
    partial class SOHnowanint
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("BonelessPharmacyBackend.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.ItemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ItemTypes");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Suffix")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("OrderStatus");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.SalesItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("MeasurementId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<int>("StockOnHand");

                    b.Property<string>("SupplierCode");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementId");

                    b.ToTable("SalesItems");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.SalesRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ItemId");

                    b.Property<int>("Quantity");

                    b.Property<int>("SaleId");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("SaleId");

                    b.ToTable("SalesRecords");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ABN")
                        .IsRequired();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("BonelessPharmacyBackend.SalesItem", b =>
                {
                    b.HasOne("BonelessPharmacyBackend.Measurement", "Measurement")
                        .WithMany()
                        .HasForeignKey("MeasurementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BonelessPharmacyBackend.SalesRecord", b =>
                {
                    b.HasOne("BonelessPharmacyBackend.SalesItem", "SalesItem")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BonelessPharmacyBackend.Sale", "Sale")
                        .WithMany("Contents")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BonelessPharmacyBackend.Staff", b =>
                {
                    b.HasOne("BonelessPharmacyBackend.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
