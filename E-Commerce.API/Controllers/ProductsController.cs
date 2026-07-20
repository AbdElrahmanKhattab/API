using E_Commerce.Application.DTOs;
using E_Commerce.Application.Services;
using E_Commerce.Application.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(Pagination<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams parameters) => Ok(await productService.GetProductsAsync(parameters));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands() => Ok(await productService.GetAllBrandsAsync());

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes() => Ok(await productService.GetAllTypesAsync());
}
