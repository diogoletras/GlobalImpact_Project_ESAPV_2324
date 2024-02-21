using GlobalImpact.Enumerates;
using GlobalImpact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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


            builder.Entity<RecyclingBinType>().HasData(
                new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.metal.ToString() },
                new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.glass.ToString() },
                new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.organic.ToString() },
                new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.paper.ToString() },
                new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.plastic.ToString() }
            );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = RoleType.client.ToString(), NormalizedName = RoleType.client.ToString().ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole {Id = Guid.NewGuid().ToString(), Name = RoleType.admin.ToString() , NormalizedName = RoleType.admin.ToString().ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()}
            );
        }
    }
}
