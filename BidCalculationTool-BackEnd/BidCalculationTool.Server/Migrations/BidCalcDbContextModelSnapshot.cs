﻿// <auto-generated />
using System;
using BidCalculationTool.Server.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BidCalculationTool.Server.Migrations
{
    [DbContext(typeof(BidCalcDbContext))]
    partial class BidCalcDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BidCalculationTool.Server.DbContext.Entities.AssociationFeeRule", b =>
                {
                    b.Property<int>("AssociationFeeRuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssociationFeeRuleId"), 1L, 1);

                    b.Property<decimal>("FeeAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("MaxPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AssociationFeeRuleId");

                    b.ToTable("AssociationFeeRules");

                    b.HasData(
                        new
                        {
                            AssociationFeeRuleId = 1,
                            FeeAmount = 5m,
                            MaxPrice = 500m,
                            MinPrice = 1m
                        },
                        new
                        {
                            AssociationFeeRuleId = 2,
                            FeeAmount = 10m,
                            MaxPrice = 1000m,
                            MinPrice = 501m
                        },
                        new
                        {
                            AssociationFeeRuleId = 3,
                            FeeAmount = 15m,
                            MaxPrice = 3000m,
                            MinPrice = 1001m
                        },
                        new
                        {
                            AssociationFeeRuleId = 4,
                            FeeAmount = 20m,
                            MinPrice = 3001m
                        });
                });

            modelBuilder.Entity("BidCalculationTool.Server.DbContext.Entities.BuyerFeeRule", b =>
                {
                    b.Property<int>("BuyerFeeRuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BuyerFeeRuleId"), 1L, 1);

                    b.Property<decimal>("MaxFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VehicleTypeId")
                        .HasColumnType("int");

                    b.HasKey("BuyerFeeRuleId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("BuyerFeeRules");

                    b.HasData(
                        new
                        {
                            BuyerFeeRuleId = 1,
                            MaxFee = 50m,
                            MinFee = 10m,
                            Percentage = 10m,
                            VehicleTypeId = 1
                        },
                        new
                        {
                            BuyerFeeRuleId = 2,
                            MaxFee = 200m,
                            MinFee = 25m,
                            Percentage = 10m,
                            VehicleTypeId = 2
                        });
                });

            modelBuilder.Entity("BidCalculationTool.Server.DbContext.Entities.SellerFeeRule", b =>
                {
                    b.Property<int>("SellerFeeRuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SellerFeeRuleId"), 1L, 1);

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VehicleTypeId")
                        .HasColumnType("int");

                    b.HasKey("SellerFeeRuleId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("SellerFeeRules");

                    b.HasData(
                        new
                        {
                            SellerFeeRuleId = 1,
                            Percentage = 2m,
                            VehicleTypeId = 1
                        },
                        new
                        {
                            SellerFeeRuleId = 2,
                            Percentage = 4m,
                            VehicleTypeId = 2
                        });
                });

            modelBuilder.Entity("BidCalculationTool.Server.DbContext.Entities.StorageFee", b =>
                {
                    b.Property<int>("StorageFeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StorageFeeId"), 1L, 1);

                    b.Property<decimal>("FeeAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("StorageFeeId");

                    b.ToTable("StorageFees");

                    b.HasData(
                        new
                        {
                            StorageFeeId = 1,
                            FeeAmount = 100m
                        });
                });

            modelBuilder.Entity("BidCalculationTool.Server.DbContext.Entities.VehicleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("VehicleTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeName = "Common"
                        },
                        new
                        {
                            Id = 2,
                            TypeName = "Luxury"
                        });
                });

            modelBuilder.Entity("BidCalculationTool.Server.DbContext.Entities.BuyerFeeRule", b =>
                {
                    b.HasOne("BidCalculationTool.Server.DbContext.Entities.VehicleType", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("BidCalculationTool.Server.DbContext.Entities.SellerFeeRule", b =>
                {
                    b.HasOne("BidCalculationTool.Server.DbContext.Entities.VehicleType", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleType");
                });
#pragma warning restore 612, 618
        }
    }
}
