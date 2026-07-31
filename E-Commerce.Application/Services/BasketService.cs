using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;

    public BasketService(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        var basket = await _basketRepository.GetBasketAsync(basketId);
        return basket ?? new CustomerBasket(basketId);
    }

    public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket)
    {
        return _basketRepository.CreateOrUpdateBasketAsync(basket);
    }

    public Task<bool> DeleteBasketAsync(string basketId)
    {
        return _basketRepository.DeleteBasketAsync(basketId);
    }
}
