using System.Text.Json;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Data;

public static class StoreDbContextSeed
{
    public static async Task SeedAsync(StoreDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        var basePath = AppContext.BaseDirectory;

        if (!await context.ProductBrands.AnyAsync())
        {
            var brands = await ReadSeedFileAsync<List<ProductBrand>>(basePath, "brands.json");
            await context.ProductBrands.AddRangeAsync(brands);
        }

        if (!await context.ProductTypes.AnyAsync())
        {
            var types = await ReadSeedFileAsync<List<ProductType>>(basePath, "types.json");
            await context.ProductTypes.AddRangeAsync(types);
        }

        if (!await context.Products.AnyAsync())
        {
            var products = await ReadSeedFileAsync<List<Product>>(basePath, "products.json");
            await context.Products.AddRangeAsync(products);
        }

        if (!await context.DeliveryMethods.AnyAsync())
        {
            await context.DeliveryMethods.AddRangeAsync(
                new DeliveryMethod { ShortName = "UPS1", Description = "Fastest delivery time", DeliveryTime = "1-2 Days", Price = 100 },
                new DeliveryMethod { ShortName = "UPS2", Description = "Get it within 5 days", DeliveryTime = "2-5 Days", Price = 50 },
                new DeliveryMethod { ShortName = "UPS3", Description = "Slower but cheap", DeliveryTime = "5-10 Days", Price = 20 },
                new DeliveryMethod { ShortName = "FREE", Description = "Free delivery for patient customers", DeliveryTime = "1-2 Weeks", Price = 0 });
        }

        await context.SaveChangesAsync();
    }

    private static async Task<T> ReadSeedFileAsync<T>(string basePath, string fileName)
    {
        var filePath = Path.Combine(basePath, "SeedData", fileName);
        var json = await File.ReadAllTextAsync(filePath);

        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}
