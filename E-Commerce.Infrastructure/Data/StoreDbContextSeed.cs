using System.Text.Json;
using E_Commerce.Domain.Entities;
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
