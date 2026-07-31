using E_Commerce.Application.Services;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketsController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasket([FromQuery] string id)
    {
        var basket = await _basketService.GetBasketAsync(id);
        return Ok(basket);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasket basket)
    {
        var createdBasket = await _basketService.CreateOrUpdateBasketAsync(basket);
        return createdBasket is null ? BadRequest() : Ok(createdBasket);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBasket(string id)
    {
        var deleted = await _basketService.DeleteBasketAsync(id);
        return deleted ? Ok(true) : BadRequest();
    }
}
