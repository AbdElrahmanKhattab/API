using System.Text.Json;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Data;

public static class StoreDbContextSeed
{
    public static async Task SeedAsync(StoreDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var seedPath = Path.Combine(AppContext.BaseDirectory, "SeedData");

        if (!await context.ProductBrands.AnyAsync())
        {
            var brands = await ReadAsync<List<ProductBrand>>(Path.Combine(seedPath, "brands.json"), options);
            if (brands is not null) await context.ProductBrands.AddRangeAsync(brands);
        }
        if (!await context.ProductTypes.AnyAsync())
        {
            var types = await ReadAsync<List<ProductType>>(Path.Combine(seedPath, "types.json"), options);
            if (types is not null) await context.ProductTypes.AddRangeAsync(types);
        }
        await context.SaveChangesAsync();

        if (!await context.Products.AnyAsync())
        {
            var products = await ReadAsync<List<Product>>(Path.Combine(seedPath, "products.json"), options);
            if (products is not null) await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }

    private static async Task<T?> ReadAsync<T>(string path, JsonSerializerOptions options) =>
        JsonSerializer.Deserialize<T>(await File.ReadAllTextAsync(path), options);
}
