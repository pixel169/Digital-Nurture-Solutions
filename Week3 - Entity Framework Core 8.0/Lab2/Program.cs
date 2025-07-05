using Lab2.Data;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("=== Retail Inventory System ===\n");

try
{
    using var context = new AppDbContext();

    // Ensure database is created
    await context.Database.EnsureCreatedAsync();

    // Check if data already exists
    if (!await context.Categories.AnyAsync())
    {
        Console.WriteLine("Inserting initial data...\n");

        // Create categories
        var electronics = new Category { Name = "Electronics" };
        var groceries = new Category { Name = "Groceries" };


        var clothing = new Category { Name = "Clothing" };

        await context.Categories.AddRangeAsync(electronics, groceries, clothing);
        // Create products
        var products = new List<Product>
        {
            new Product { Name = "Laptop", Price = 75000, Category = electronics },
            new Product { Name = "Rice Bag (25kg)", Price = 1200, Category = groceries },
            new Product { Name = "Jeans", Price = 2499, Category = clothing },
        };

        await context.Products.AddRangeAsync(products);

        // Save changes
        int recordsAffected = await context.SaveChangesAsync();
        Console.WriteLine($"Successfully inserted {recordsAffected} records!\n");
    }
    else
    {
        Console.WriteLine("Data already exists. Skipping insertion.\n");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
