using Microsoft.EntityFrameworkCore;
using OnlineShop.Configurations;
using OnlineShop.Entities;

namespace OnlineShop.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; } = null!;
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().ToTable("Products");
            modelBuilder.Entity<OrderEntity>().ToTable("Orders");
            modelBuilder.Entity<UserEntity>().ToTable("Users");

            modelBuilder.ApplyConfiguration(new OrderDbConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDbConfiguration());
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        }
    }
}
