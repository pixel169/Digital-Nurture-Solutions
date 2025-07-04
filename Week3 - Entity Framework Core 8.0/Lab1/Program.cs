using Microsoft.EntityFrameworkCore;
using RetailInventory.Data;
using RetailInventory.Models;

namespace RetailInventory
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Configure DbContext
            var options = new DbContextOptionsBuilder<RetailContext>()
                .UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=RetailInventoryDB;Trusted_Connection=true;TrustServerCertificate=true;")
                .Options;

            using var context = new RetailContext(options);

            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            Console.WriteLine("=== Retail Inventory Management System ===\n");

            // Demonstrate various ORM operations
            await DemonstrateBasicQueries(context);
            await DemonstrateLinqQueries(context);
            await DemonstrateCrudOperations(context);
            await DemonstrateRelationships(context);
            await DemonstrateAsyncOperations(context);
        }

        static async Task DemonstrateBasicQueries(RetailContext context)
        {
            Console.WriteLine("1. Basic Queries:");
            Console.WriteLine("================");

            // Get all products
            var products = await context.Products.ToListAsync();
            Console.WriteLine($"Total Products: {products.Count}");

            // Get products with categories
            var productsWithCategories = await context.Products
                .Include(p => p.Category)
                .ToListAsync();

            foreach (var product in productsWithCategories)
            {
                Console.WriteLine($"- {product.Name} (${product.Price}) - Category: {product.Category.Name}");
            }
            Console.WriteLine();
        }

        static async Task DemonstrateLinqQueries(RetailContext context)
        {
            Console.WriteLine("2. LINQ Queries:");
            Console.WriteLine("================");

            // Products above certain price
            var expensiveProducts = await context.Products
                .Where(p => p.Price > 30)
                .OrderByDescending(p => p.Price)
                .ToListAsync();

            Console.WriteLine("Products above $30:");
            foreach (var product in expensiveProducts)
            {
                Console.WriteLine($"- {product.Name}: ${product.Price}");
            }

            // Category with most products
            var categoryStats = await context.Categories
                .Select(c => new { c.Name, ProductCount = c.Products.Count })
                .OrderByDescending(x => x.ProductCount)
                .FirstOrDefaultAsync();

            Console.WriteLine($"\nCategory with most products: {categoryStats?.Name} ({categoryStats?.ProductCount} products)");
            Console.WriteLine();
        }

        static async Task DemonstrateCrudOperations(RetailContext context)
        {
            Console.WriteLine("3. CRUD Operations:");
            Console.WriteLine("==================");

            // CREATE - Add new product
            var newProduct = new Product
            {
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse",
                Price = 29.99m,
                CategoryId = 1
            };

            context.Products.Add(newProduct);
            await context.SaveChangesAsync();
            Console.WriteLine($"Created product: {newProduct.Name} (ID: {newProduct.Id})");

            // READ - Find product by ID
            var foundProduct = await context.Products.FindAsync(newProduct.Id);
            Console.WriteLine($"Found product: {foundProduct?.Name}");

            // UPDATE - Modify product
            if (foundProduct != null)
            {
                foundProduct.Price = 24.99m;
                foundProduct.LastModified = DateTime.UtcNow;
                await context.SaveChangesAsync();
                Console.WriteLine($"Updated product price to: ${foundProduct.Price}");
            }

            // DELETE - Remove product
            if (foundProduct != null)
            {
                context.Products.Remove(foundProduct);
                await context.SaveChangesAsync();
                Console.WriteLine("Product deleted successfully");
            }
            Console.WriteLine();
        }

        static async Task DemonstrateRelationships(RetailContext context)
        {
            Console.WriteLine("4. Relationships:");
            Console.WriteLine("=================");

            // Products with stock levels
            var productsWithStock = await context.Products
                .Include(p => p.StockLevels)
                .Include(p => p.Category)
                .ToListAsync();

            foreach (var product in productsWithStock)
            {
                Console.WriteLine($"{product.Name} - {product.Category.Name}:");
                foreach (var stock in product.StockLevels)
                {
                    var status = stock.IsLowStock ? " (LOW STOCK!)" : "";
                    Console.WriteLine($"  - {stock.Location}: {stock.Quantity} units{status}");
                }
                Console.WriteLine();
            }
        }

        static async Task DemonstrateAsyncOperations(RetailContext context)
        {
            Console.WriteLine("5. Async Operations:");
            Console.WriteLine("===================");

            // Async query with complex filtering
            var lowStockProducts = await context.Products
                .Where(p => p.StockLevels.Any(s => s.Quantity <= s.MinimumStock))
                .Include(p => p.StockLevels)
                .Include(p => p.Category)
                .ToListAsync();

            Console.WriteLine("Products with low stock:");
            foreach (var product in lowStockProducts)
            {
                var lowStockLocations = product.StockLevels
                    .Where(s => s.IsLowStock)
                    .Select(s => s.Location);
                Console.WriteLine($"- {product.Name}: {string.Join(", ", lowStockLocations)}");
            }

            // Async aggregation
            var totalInventoryValue = await context.Products
                .SumAsync(p => p.Price * p.StockLevels.Sum(s => s.Quantity));

            Console.WriteLine($"\nTotal Inventory Value: ${totalInventoryValue:F2}");
            Console.WriteLine();
        }
    }
}