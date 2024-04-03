﻿// <auto-generated />
using System;
using GlobalImpact.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GlobalImpact.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240402194150_SGRR")]
    partial class SGRR
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GlobalImpact.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("NIF")
                        .HasColumnType("int");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UniqueCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "08345297-5853-4b89-ad62-6a1b23c00dcc",
                            AccessFailedCount = 0,
                            Age = 0,
                            ConcurrencyStamp = "ee1d8506-8be4-40f5-8d03-bc549f5ac908",
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            LastName = "Admin",
                            LockoutEnabled = true,
                            NIF = 0,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEM/dVKiYnhadx6Ev4VYV6MUn5OGL7008maXMXcesMEOrKpKQ4nUHjkZpQTPUFvUp1A==",
                            PhoneNumber = "123456789",
                            PhoneNumberConfirmed = true,
                            Points = 10000,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UniqueCode = "f101563e-c9c7-4a7f-8bbc-066437d7344f",
                            UserName = "admin"
                        },
                        new
                        {
                            Id = "42003a85-23eb-4a34-bee0-66f05ab91e61",
                            AccessFailedCount = 0,
                            Age = 50,
                            ConcurrencyStamp = "1edcb778-69c4-46d6-9cbf-b47912c9ea92",
                            Email = "cliente@exemplo.com",
                            EmailConfirmed = true,
                            FirstName = "Cliente",
                            LastName = "Cliente",
                            LockoutEnabled = true,
                            NIF = 0,
                            NormalizedEmail = "CLIENTE@EXEMPLO.COM",
                            NormalizedUserName = "CLIENTE",
                            PasswordHash = "AQAAAAIAAYagAAAAEKt9B93zAq7ZTNd7VZe/jd5znuwkQfUNszASCHo8i9hq75I2aQRuFKuNbiE0OmxU5A==",
                            PhoneNumber = "123456789",
                            PhoneNumberConfirmed = true,
                            Points = 10000,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UniqueCode = "cc5ec226-2e94-40f4-a0a2-036fae335413",
                            UserName = "cliente"
                        });
                });

            modelBuilder.Entity("GlobalImpact.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("ProductCategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2b39fbec-4835-42ec-80df-24cf0e2d404d"),
                            Description = "Costoletas de Vaca",
                            ImageUrl = "Talho-Castro-Costeleta-Porco.jpg",
                            Name = "Costoletas",
                            Points = 5,
                            ProductCategoryId = "dfe993ee-e166-4b37-a7b0-3d1a79d73dcf",
                            Stock = 20
                        },
                        new
                        {
                            Id = new Guid("195f93f8-1169-4053-89ea-3af890734743"),
                            Description = "Bacalhau da Noruega",
                            ImageUrl = "bacalhau.jpg",
                            Name = "Bacalhau",
                            Points = 8,
                            ProductCategoryId = "896430dc-6dec-4bc6-a800-f57102bdde38",
                            Stock = 25
                        },
                        new
                        {
                            Id = new Guid("0a656c7f-6a93-4d93-b0a6-709e6cc59435"),
                            Description = "Broculos Verde",
                            ImageUrl = "broculos.jpg",
                            Name = "Broculos",
                            Points = 1,
                            ProductCategoryId = "c46c467c-2c2a-406f-933a-d94e1bb1d708",
                            Stock = 50
                        },
                        new
                        {
                            Id = new Guid("28018811-e2ac-44e8-a87f-2da208e8be94"),
                            Description = "Pessego da Colombia",
                            ImageUrl = "pessego.jpg",
                            Name = "Pessego",
                            Points = 2,
                            ProductCategoryId = "bd25d7bf-2191-4cc9-85de-56dd53305046",
                            Stock = 30
                        },
                        new
                        {
                            Id = new Guid("2e521fb1-f2e2-4a9f-b632-23483ec58a31"),
                            Description = "Licor Beirao versao Especial 100 anos",
                            ImageUrl = "licro-beirao.jpg",
                            Name = "Licor Beirao",
                            Points = 11,
                            ProductCategoryId = "36c8176f-a513-4e8b-bdb4-5cf3af9f8e75",
                            Stock = 5
                        });
                });

            modelBuilder.Entity("GlobalImpact.Models.ProductCategory", b =>
                {
                    b.Property<Guid>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductCategoryId");

                    b.ToTable("ProductsCategory");

                    b.HasData(
                        new
                        {
                            ProductCategoryId = new Guid("dfe993ee-e166-4b37-a7b0-3d1a79d73dcf"),
                            Category = "talho"
                        },
                        new
                        {
                            ProductCategoryId = new Guid("896430dc-6dec-4bc6-a800-f57102bdde38"),
                            Category = "peixaria"
                        },
                        new
                        {
                            ProductCategoryId = new Guid("c46c467c-2c2a-406f-933a-d94e1bb1d708"),
                            Category = "legumes"
                        },
                        new
                        {
                            ProductCategoryId = new Guid("bd25d7bf-2191-4cc9-85de-56dd53305046"),
                            Category = "frutas"
                        },
                        new
                        {
                            ProductCategoryId = new Guid("36c8176f-a513-4e8b-bdb4-5cf3af9f8e75"),
                            Category = "bebidas"
                        });
                });

            modelBuilder.Entity("GlobalImpact.Models.ProductTransactionStatus", b =>
                {
                    b.Property<Guid>("ProductTransactionStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductTransactionStatusId");

                    b.ToTable("ProductTransactionStatus");

                    b.HasData(
                        new
                        {
                            ProductTransactionStatusId = new Guid("ec00cd92-5e26-473b-8060-0fb2f9853a68"),
                            Status = "Completed"
                        },
                        new
                        {
                            ProductTransactionStatusId = new Guid("6a29600e-1b3a-4726-9243-07c3685279fd"),
                            Status = "Pending"
                        },
                        new
                        {
                            ProductTransactionStatusId = new Guid("1c065574-b70c-4c49-8949-01788d0cc0c2"),
                            Status = "Cancelled"
                        });
                });

            modelBuilder.Entity("GlobalImpact.Models.ProductTransactions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ProductTransactions");
                });

            modelBuilder.Entity("GlobalImpact.Models.RecyclingBin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Capacity")
                        .HasColumnType("float");

                    b.Property<double>("CurrentCapacity")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("RecyclingBinTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("RecyclingBins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2e9e099c-a34a-40d6-8e53-fb0a2ab7cb3c"),
                            Capacity = 100.0,
                            CurrentCapacity = 0.0,
                            Description = "Recycling Bin Glass",
                            Latitude = 38.521607817359822,
                            Longitude = -8.8368159603671987,
                            RecyclingBinTypeId = "ca51ea2c-94df-46c1-8c34-95cc31e720fd",
                            Status = false
                        },
                        new
                        {
                            Id = new Guid("bf4028b2-0729-4be7-981d-5047faf7bc4e"),
                            Capacity = 100.0,
                            CurrentCapacity = 0.0,
                            Description = "Recycling Bin Plastic",
                            Latitude = 38.52171490188254,
                            Longitude = -8.83694281687076,
                            RecyclingBinTypeId = "6b131442-388a-491f-9216-9da450a055bd",
                            Status = false
                        },
                        new
                        {
                            Id = new Guid("9ce798ab-5d60-4229-8c65-df9d9c9ce54f"),
                            Capacity = 100.0,
                            CurrentCapacity = 0.0,
                            Description = "Recycling Bin Paper",
                            Latitude = 38.521474614438482,
                            Longitude = -8.8366557205732299,
                            RecyclingBinTypeId = "494e9f48-4598-45df-a7e4-91f2fe2d0055",
                            Status = false
                        },
                        new
                        {
                            Id = new Guid("5be8de11-4493-4c42-bcff-daf9f6bee6e1"),
                            Capacity = 100.0,
                            CurrentCapacity = 0.0,
                            Description = "Recycling Bin Glass",
                            Latitude = 38.519799793743871,
                            Longitude = -8.8360971667515606,
                            RecyclingBinTypeId = "ca51ea2c-94df-46c1-8c34-95cc31e720fd",
                            Status = false
                        },
                        new
                        {
                            Id = new Guid("302268d0-5a63-4544-bfd6-658f043d6003"),
                            Capacity = 100.0,
                            CurrentCapacity = 100.0,
                            Description = "Recycling Bin Plastic",
                            Latitude = 38.522550713957862,
                            Longitude = -8.8395605732421387,
                            RecyclingBinTypeId = "6b131442-388a-491f-9216-9da450a055bd",
                            Status = true
                        },
                        new
                        {
                            Id = new Guid("0bc21829-4deb-4eca-9805-ea1df30a5a2f"),
                            Capacity = 100.0,
                            CurrentCapacity = 100.0,
                            Description = "Recycling Bin Paper",
                            Latitude = 38.522682016378347,
                            Longitude = -8.8397580181150541,
                            RecyclingBinTypeId = "494e9f48-4598-45df-a7e4-91f2fe2d0055",
                            Status = true
                        });
                });

            modelBuilder.Entity("GlobalImpact.Models.RecyclingBinType", b =>
                {
                    b.Property<Guid>("RecyclingBinTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecyclingBinTypeId");

                    b.ToTable("RecyclingBinType");

                    b.HasData(
                        new
                        {
                            RecyclingBinTypeId = new Guid("ca51ea2c-94df-46c1-8c34-95cc31e720fd"),
                            Type = "glass"
                        },
                        new
                        {
                            RecyclingBinTypeId = new Guid("6b131442-388a-491f-9216-9da450a055bd"),
                            Type = "plastic"
                        },
                        new
                        {
                            RecyclingBinTypeId = new Guid("494e9f48-4598-45df-a7e4-91f2fe2d0055"),
                            Type = "paper"
                        });
                });

            modelBuilder.Entity("GlobalImpact.Models.RecyclingTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<Guid>("RecyclingBinId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("RecyclingBinId");

                    b.HasIndex("UserId");

                    b.ToTable("RecyclingTransactions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "bf60fa39-50ed-4f64-8e8a-0adf5dd5e9c7",
                            ConcurrencyStamp = "73a01f9f-a1ab-4cbd-8742-e064b2c8debe",
                            Name = "client",
                            NormalizedName = "CLIENT"
                        },
                        new
                        {
                            Id = "862a3e4b-48f2-48b5-92a5-d633a4424185",
                            ConcurrencyStamp = "6d9713b0-8205-4629-a5ff-2c431f2ee28a",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "42003a85-23eb-4a34-bee0-66f05ab91e61",
                            RoleId = "bf60fa39-50ed-4f64-8e8a-0adf5dd5e9c7"
                        },
                        new
                        {
                            UserId = "08345297-5853-4b89-ad62-6a1b23c00dcc",
                            RoleId = "862a3e4b-48f2-48b5-92a5-d633a4424185"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GlobalImpact.Models.RecyclingTransaction", b =>
                {
                    b.HasOne("GlobalImpact.Models.RecyclingBin", "RecyclingBin")
                        .WithMany()
                        .HasForeignKey("RecyclingBinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GlobalImpact.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecyclingBin");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GlobalImpact.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GlobalImpact.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GlobalImpact.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GlobalImpact.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}