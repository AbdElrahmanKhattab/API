using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Services;

public interface IBasketService
{
    Task<CustomerBasket> GetBasketAsync(string basketId);
    Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string basketId);
}
