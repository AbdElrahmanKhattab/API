using System.Text.Json;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace E_Commerce.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private static readonly TimeSpan DefaultBasketTimeToLive = TimeSpan.FromDays(30);
    private readonly IDistributedCache _cache;

    public BasketRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<CustomerBasket?> GetBasketAsync(string basketId)
    {
        var basketJson = await _cache.GetStringAsync(basketId);
        return string.IsNullOrWhiteSpace(basketJson) ? null : JsonSerializer.Deserialize<CustomerBasket>(basketJson);
    }

    public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
    {
        var basketJson = JsonSerializer.Serialize(basket);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = timeToLive ?? DefaultBasketTimeToLive
        };

        await _cache.SetStringAsync(basket.Id, basketJson, options);
        return await GetBasketAsync(basket.Id);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        await _cache.RemoveAsync(basketId);
        return await GetBasketAsync(basketId) is null;
    }
}
