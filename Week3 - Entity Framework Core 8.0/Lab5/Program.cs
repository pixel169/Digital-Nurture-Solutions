using Lab2.Data;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("=== Data Retrieval Examples ===\n");

try
{
    using var context = new AppDbContext();

    // 1. Retrieve All Products
    Console.WriteLine("📋 ALL PRODUCTS:");
    Console.WriteLine("".PadRight(50, '-'));

    var products = await context.Products
        .Include(p => p.Category)
        .ToListAsync();

    foreach (var product in products)
    {
        Console.WriteLine($"{product.Name} - ₹{product.Price:N0} ({product.Category.Name})");
    }

    // 2. Find by ID
    Console.WriteLine($"\n🔍 FIND BY ID (ID: 1):");
    Console.WriteLine("".PadRight(50, '-'));

    var productById = await context.Products
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.Id == 1);

    if (productById != null)
    {
        Console.WriteLine($"Found: {productById.Name} - {productById.Price:N0}");
    }
    else
    {
        Console.WriteLine("Product not found!");
    }

    // 3. FirstOrDefault with Condition
    Console.WriteLine($"\n EXPENSIVE PRODUCTS (Price > ₹50,000):");
    Console.WriteLine("".PadRight(50, '-'));

    var expensiveProducts = await context.Products
        .Include(p => p.Category)
        .Where(p => p.Price > 50000)
        .ToListAsync();

    if (expensiveProducts.Any())
    {
        foreach (var product in expensiveProducts)
        {
            Console.WriteLine($" {product.Name} - ₹{product.Price:N0}");
        }
    }
    else
    {
        Console.WriteLine("No expensive products found!");
    }

    // 4. Count and Statistics
    Console.WriteLine($"\n STATISTICS:");
    Console.WriteLine("".PadRight(50, '-'));

    var totalProducts = await context.Products.CountAsync();
    var totalCategories = await context.Categories.CountAsync();
    var avgPrice = await context.Products.AverageAsync(p => p.Price);

    Console.WriteLine($"Total Products: {totalProducts}");
    Console.WriteLine($"Total Categories: {totalCategories}");
    Console.WriteLine($"Average Price: ₹{avgPrice:N2}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}