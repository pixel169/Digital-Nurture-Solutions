using Microsoft.EntityFrameworkCore;
using RetailInventory.Models;

namespace RetailInventory.Data
{
    public class RetailContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StockLevel> StockLevels { get; set; }

        public RetailContext(DbContextOptions<RetailContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(10,2)");

                // Configure relationship with Category
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure relationship with StockLevels
                entity.HasMany(p => p.StockLevels)
                      .WithOne(s => s.Product)
                      .HasForeignKey(s => s.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
                entity.HasIndex(c => c.Name).IsUnique();
            });

            // Configure StockLevel entity
            modelBuilder.Entity<StockLevel>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Location).HasMaxLength(50);

                // Create composite index for better performance
                entity.HasIndex(s => new { s.ProductId, s.Location }).IsUnique();
            });

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and accessories" },
                new Category { Id = 2, Name = "Clothing", Description = "Apparel and fashion items" },
                new Category { Id = 3, Name = "Books", Description = "Books and educational materials" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 999.99m, CategoryId = 1 },
                new Product { Id = 2, Name = "T-Shirt", Description = "Cotton t-shirt", Price = 19.99m, CategoryId = 2 },
                new Product { Id = 3, Name = "Programming Book", Description = "C# Programming Guide", Price = 49.99m, CategoryId = 3 }
            );

            // Seed Stock Levels
            modelBuilder.Entity<StockLevel>().HasData(
                new StockLevel { Id = 1, ProductId = 1, Location = "Warehouse A", Quantity = 15, MinimumStock = 5 },
                new StockLevel { Id = 2, ProductId = 1, Location = "Store Front", Quantity = 3, MinimumStock = 2 },
                new StockLevel { Id = 3, ProductId = 2, Location = "Warehouse A", Quantity = 50, MinimumStock = 10 },
                new StockLevel { Id = 4, ProductId = 3, Location = "Store Front", Quantity = 8, MinimumStock = 3 }
            );
        }
    }
}