using System.Drawing.Text;
using System.Security.Cryptography;
using GlobalImpact.Enumerates;
using GlobalImpact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GlobalImpact.ViewModels.NewFolder;
using GlobalImpact.ViewModels.Account;

namespace GlobalImpact.Data
{
    /// <summary>
    /// Classe de Auxílio à migração dos dados.
    /// </summary>
    public partial class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<RecyclingBin> RecyclingBins { get; set; }
        public DbSet<RecyclingTransaction> RecyclingTransactions { get; set; }
        public DbSet<RecyclingBinType> RecyclingBinType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var rb1ID = Guid.NewGuid();
            var rb2ID = Guid.NewGuid();
            var rb3ID = Guid.NewGuid();

            RecyclingBinType rb1 = new RecyclingBinType
            {
                RecyclingBinTypeId = rb1ID,
                Type = BinType.glass.ToString()
            };
            RecyclingBinType rb2 = new RecyclingBinType
            {
                RecyclingBinTypeId = rb2ID,
                Type = BinType.plastic.ToString()
            };
            RecyclingBinType rb3 = new RecyclingBinType
            {
                RecyclingBinTypeId = rb3ID,
                Type = BinType.paper.ToString()
            };

            builder.Entity<RecyclingBinType>().HasData(
                rb1,
                rb2,
                rb3
            );

            Guid adminRoleId = Guid.NewGuid();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = RoleType.client.ToString(),
                    NormalizedName = RoleType.client.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = adminRoleId.ToString(),
                    Name = RoleType.admin.ToString(),
                    NormalizedName = RoleType.admin.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            Guid userId = Guid.NewGuid();

            builder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = userId.ToString(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin123"),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = "123456789",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UniqueCode = Guid.NewGuid().ToString(),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Age = 0,
                    Points = 0,
                    NIF = 0
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId.ToString(),
                    UserId = userId.ToString()
                });

            builder.Entity<RecyclingBin>().HasData(
                new RecyclingBin
                {
                    Id = Guid.NewGuid(),
                    RecyclingBinTypeId = rb1ID.ToString(),
                    Latitude = 38.52160781735982,
                    Longitude = -8.836815960367199,
                    Description = "Recycling Bin Glass",
                    Capacity = 100,
                    CurrentCapacity = 0,
                    Status = true
                },
                new RecyclingBin
                {
                    Id = Guid.NewGuid(),
                    RecyclingBinTypeId = rb2ID.ToString(),
                    Latitude = 38.52171490188254,
                    Longitude = -8.83694281687076,
                    Description = "Recycling Bin Plastic",
                    Capacity = 100,
                    CurrentCapacity = 0,
                    Status = true
                },
                new RecyclingBin
                {
                    Id = Guid.NewGuid(),
                    RecyclingBinTypeId = rb3ID.ToString(),
                    Latitude = 38.52147461443848,
                    Longitude = -8.83665572057323,
                    Description = "Recycling Bin Paper",
                    Capacity = 100,
                    CurrentCapacity = 0,
                    Status = true
                },
                new RecyclingBin
                {
                    Id = Guid.NewGuid(),
                    RecyclingBinTypeId = rb1ID.ToString(),
                    Latitude = 38.51979979374387,
                    Longitude = -8.83609716675156,
                    Description = "Recycling Bin Glass",
                    Capacity = 100,
                    CurrentCapacity = 0,
                    Status = true
                },
                new RecyclingBin
                {
                    Id = Guid.NewGuid(),
                    RecyclingBinTypeId = rb2ID.ToString(),
                    Latitude = 38.52255071395786,
                    Longitude = -8.839560573242139,
                    Description = "Recycling Bin Plastic",
                    Capacity = 100,
                    CurrentCapacity = 0,
                    Status = true
                },
                new RecyclingBin
                {
                    Id = Guid.NewGuid(),
                    RecyclingBinTypeId = rb3ID.ToString(),
                    Latitude = 38.52268201637835,
                    Longitude = -8.839758018115054,
                    Description = "Recycling Bin Paper",
                    Capacity = 100,
                    CurrentCapacity = 0,
                    Status = true
                }
            );
        }
    }
}
