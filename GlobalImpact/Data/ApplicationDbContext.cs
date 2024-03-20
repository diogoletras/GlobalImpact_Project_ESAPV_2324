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
		public DbSet<ProductCategory> ProductsCategory { get; set; }
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

			var pc1ID = Guid.NewGuid();
			var pc2ID = Guid.NewGuid();
			var pc3ID = Guid.NewGuid();
			var pc4ID = Guid.NewGuid();
			var pc5ID = Guid.NewGuid();

            ProductCategory pc1 = new ProductCategory
            {
                ProductCategoryId = pc1ID,
                Category = ProductType.talho.ToString()
			};
			ProductCategory pc2 = new ProductCategory
			{
				ProductCategoryId = pc2ID,
				Category = ProductType.peixaria.ToString()
	};
			ProductCategory pc3 = new ProductCategory
			{
				ProductCategoryId = pc3ID,
				Category = ProductType.legumes.ToString()
			};
			ProductCategory pc4 = new ProductCategory
			{
				ProductCategoryId = pc4ID,
				Category = ProductType.frutas.ToString()
			};
			ProductCategory pc5 = new ProductCategory
			{
				ProductCategoryId = pc5ID,
				Category = ProductType.bebidas.ToString()
			};

			builder.Entity<ProductCategory>().HasData(
				pc1,
				pc2,
				pc3,
				pc4,
				pc5
			);

			builder.Entity<Product>().HasData(
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "Costoletas",
					Description = "Costoletas de Vaca",
					Price = 5,
					Tax = 0.06,
					Stock = 20,
					ProductCategoryId = pc1ID.ToString(),
                    ImageUrl = "Talho-Castro-Costeleta-Porco.jpg"
                },
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "Bacalhau",
					Description = "Bacalhau da Noruega",
					Price = 8,
					Tax = 0.06,
					Stock = 25,
					ProductCategoryId = pc2ID.ToString(),
                    ImageUrl = "bacalhau.jpg"
                },
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "Broculos",
					Description = "Broculos Verde",
					Price = 1.5,
					Tax = 0.06,
					Stock = 50,
					ProductCategoryId = pc3ID.ToString(),
                    ImageUrl = "broculos.jpg"
                },
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "Pessego",
					Description = "Pessego da Colombia",
					Price = 2.3,
					Tax = 0.06,
					Stock = 30,
					ProductCategoryId = pc4ID.ToString(),
                    ImageUrl = "pessego.jpg"
                },
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "Licor Beirao",
					Description = "Licor Beirao versao Especial 100 anos",
					Price = 11.2,
					Tax = 0.23,
					Stock = 5,
					ProductCategoryId = pc5ID.ToString(),
                    ImageUrl = "licro-beirao.jpg"
                }
			);
		}
    }
}
