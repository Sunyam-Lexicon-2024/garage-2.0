﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using garage_2._0.Models;

#nullable disable

namespace garage_2._0.Migrations
{
    [DbContext(typeof(GarageDbContext))]
    [Migration("20240418161252_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("garage_2._0.Models.Garage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Garages");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            MaxCapacity = 50,
                            Name = "Default Garage One"
                        });
                });

            modelBuilder.Entity("garage_2._0.Models.ParkedVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Wheels")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GarageId");

                    b.ToTable("ParkedVehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Volkswagen",
                            Color = 4,
                            GarageId = 1,
                            Model = "Unknown",
                            RegisteredAt = new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7634),
                            RegistrationNumber = "FPD941",
                            Type = 0,
                            Wheels = 4
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Saab",
                            Color = 1,
                            GarageId = 1,
                            Model = "Unknown",
                            RegisteredAt = new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7696),
                            RegistrationNumber = "CLQ415",
                            Type = 0,
                            Wheels = 4
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Volvo",
                            Color = 2,
                            GarageId = 1,
                            Model = "Unknown",
                            RegisteredAt = new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7699),
                            RegistrationNumber = "YHV901",
                            Type = 0,
                            Wheels = 4
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Audi",
                            Color = 7,
                            GarageId = 1,
                            Model = "Unknown",
                            RegisteredAt = new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7701),
                            RegistrationNumber = "GBO781",
                            Type = 0,
                            Wheels = 4
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Toyota",
                            Color = 0,
                            GarageId = 1,
                            Model = "Unknown",
                            RegisteredAt = new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7704),
                            RegistrationNumber = "JRC132",
                            Type = 0,
                            Wheels = 4
                        });
                });

            modelBuilder.Entity("garage_2._0.Models.ParkedVehicle", b =>
                {
                    b.HasOne("garage_2._0.Models.Garage", "Garage")
                        .WithMany("ParkedVehicles")
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("garage_2._0.Models.Garage", b =>
                {
                    b.Navigation("ParkedVehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
