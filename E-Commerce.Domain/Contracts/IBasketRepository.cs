using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string basketId);
    Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null);
    Task<bool> DeleteBasketAsync(string basketId);
}
