using InventoryManagmentSystem.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagmentSystem.Core.Data
{
    public class InventoryContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryTransaction> Transactions { get; set; }
        public DbSet<LowStockLog> LowStockLogs { get; set; }

        public InventoryContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Inventory>().HasKey(Inv => new
            {
                Inv.ProductId,
                Inv.WarehouseId
            });

            builder.Entity<Inventory>()
                 .HasOne(Inv => Inv.Product)
                 .WithMany(w => w.Inventories)
                 .HasForeignKey(Inv => Inv.ProductId);
            builder.Entity<Inventory>()
             .HasOne(Inv => Inv.Warehouse)
             .WithMany(w => w.Inventories)
             .HasForeignKey(Inv => Inv.WarehouseId);

            builder.Entity<InventoryTransaction>()
                .HasIndex(x => x.CreatedAt);

        

        }
     
    }
}