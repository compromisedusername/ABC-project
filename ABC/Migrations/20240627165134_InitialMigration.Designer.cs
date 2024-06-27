﻿// <auto-generated />
using System;
using ABC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ABC.Migrations
{
    [DbContext(typeof(AppDatabaseContext))]
    [Migration("20240627165134_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ABC.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Address");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Warsaw",
                            Country = "Poland",
                            HouseNumber = "2",
                            Street = "Nizinna"
                        },
                        new
                        {
                            Id = 2,
                            City = "Warsaw",
                            Country = "Poland",
                            HouseNumber = "1",
                            Street = "Fabryczna"
                        });
                });

            modelBuilder.Entity("ABC.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Education",
                            Name = "Educational purposes only."
                        });
                });

            modelBuilder.Entity("ABC.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdAddress")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdAddress");

                    b.ToTable("Client");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("ABC.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("IdDiscount")
                        .HasColumnType("int");

                    b.Property<int>("IdSoftwareSystem")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSigned")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<int>("SupportUpdatePeriodInYears")
                        .HasColumnType("int");

                    b.Property<string>("UpdateInformation")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdDiscount");

                    b.HasIndex("IdSoftwareSystem");

                    b.ToTable("Contract");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateFrom = new DateTime(2024, 6, 27, 18, 51, 34, 176, DateTimeKind.Local).AddTicks(6341),
                            DateTo = new DateTime(2024, 7, 17, 18, 51, 34, 176, DateTimeKind.Local).AddTicks(6740),
                            IdClient = 1,
                            IdDiscount = 1,
                            IdSoftwareSystem = 1,
                            IsActive = false,
                            IsSigned = false,
                            Price = 3000m,
                            SupportUpdatePeriodInYears = 2,
                            UpdateInformation = "Possible updates in future"
                        });
                });

            modelBuilder.Entity("ABC.Models.ContractsSoftwareSystems", b =>
                {
                    b.Property<int>("IdContract")
                        .HasColumnType("int");

                    b.Property<int>("IdSoftwareSystem")
                        .HasColumnType("int");

                    b.HasKey("IdContract", "IdSoftwareSystem");

                    b.HasIndex("IdSoftwareSystem");

                    b.ToTable("Contarcts_SoftwareSystems");

                    b.HasData(
                        new
                        {
                            IdContract = 1,
                            IdSoftwareSystem = 1
                        });
                });

            modelBuilder.Entity("ABC.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Discount");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateFrom = new DateTime(2024, 6, 26, 18, 51, 34, 172, DateTimeKind.Local).AddTicks(1077),
                            DateTo = new DateTime(2024, 7, 7, 18, 51, 34, 175, DateTimeKind.Local).AddTicks(7247),
                            Name = "Black Friday",
                            Value = 20
                        });
                });

            modelBuilder.Entity("ABC.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("IdContract")
                        .HasColumnType("int");

                    b.Property<decimal>("MoneyAmount")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.HasKey("Id");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdContract");

                    b.ToTable("Payment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2024, 6, 27, 18, 51, 34, 176, DateTimeKind.Local).AddTicks(3564),
                            IdClient = 1,
                            IdContract = 1,
                            MoneyAmount = 1000m
                        });
                });

            modelBuilder.Entity("ABC.Models.SoftwareSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("IdCategory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PriceForYear")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<string>("VersionInformation")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.ToTable("SoftwareSystem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Educational Software for students",
                            IdCategory = 1,
                            Name = "EduSoftware",
                            PriceForYear = 2000m,
                            VersionInformation = "Version 1.0"
                        });
                });

            modelBuilder.Entity("ABC.Models.ClientCompany", b =>
                {
                    b.HasBaseType("ABC.Models.Client");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.ToTable("ClientCompany");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "example@example.com",
                            IdAddress = 1,
                            PhoneNumber = "661354887",
                            CompanyName = "CompanyName",
                            KRS = "12345678"
                        });
                });

            modelBuilder.Entity("ABC.Models.ClientNatural", b =>
                {
                    b.HasBaseType("ABC.Models.Client");

                    b.Property<string>("FristName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.ToTable("ClientNatural");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Email = "example@example2.com",
                            IdAddress = 2,
                            PhoneNumber = "887345645",
                            FristName = "Jan",
                            LastName = "Kowalski",
                            PESEL = "031276398"
                        });
                });

            modelBuilder.Entity("ABC.Models.Client", b =>
                {
                    b.HasOne("ABC.Models.Address", "Address")
                        .WithMany("Clients")
                        .HasForeignKey("IdAddress")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ABC.Models.Contract", b =>
                {
                    b.HasOne("ABC.Models.Client", "Client")
                        .WithMany("Contracts")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABC.Models.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("IdDiscount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABC.Models.SoftwareSystem", "SoftwareSystem")
                        .WithMany()
                        .HasForeignKey("IdSoftwareSystem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Discount");

                    b.Navigation("SoftwareSystem");
                });

            modelBuilder.Entity("ABC.Models.ContractsSoftwareSystems", b =>
                {
                    b.HasOne("ABC.Models.Contract", "Contract")
                        .WithMany("ContractsSoftwareSystems")
                        .HasForeignKey("IdContract")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ABC.Models.SoftwareSystem", "SoftwareSystem")
                        .WithMany("ContractsSoftwareSystems")
                        .HasForeignKey("IdSoftwareSystem")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("SoftwareSystem");
                });

            modelBuilder.Entity("ABC.Models.Payment", b =>
                {
                    b.HasOne("ABC.Models.Client", "Client")
                        .WithMany("Payments")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABC.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("IdContract")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("ABC.Models.SoftwareSystem", b =>
                {
                    b.HasOne("ABC.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ABC.Models.ClientCompany", b =>
                {
                    b.HasOne("ABC.Models.Client", null)
                        .WithOne()
                        .HasForeignKey("ABC.Models.ClientCompany", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ABC.Models.ClientNatural", b =>
                {
                    b.HasOne("ABC.Models.Client", null)
                        .WithOne()
                        .HasForeignKey("ABC.Models.ClientNatural", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ABC.Models.Address", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("ABC.Models.Client", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("ABC.Models.Contract", b =>
                {
                    b.Navigation("ContractsSoftwareSystems");
                });

            modelBuilder.Entity("ABC.Models.SoftwareSystem", b =>
                {
                    b.Navigation("ContractsSoftwareSystems");
                });
#pragma warning restore 612, 618
        }
    }
}
