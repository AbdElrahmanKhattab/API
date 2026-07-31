using E_Commerce.Application.DTOs;
using E_Commerce.Application.Specifications;

namespace E_Commerce.Application.Services;

public interface IProductService
{
    Task<Pagination<ProductDto>> GetProductsAsync(ProductSpecParams specParams);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<BrandDto>> GetBrandsAsync();
    Task<IReadOnlyList<TypeDto>> GetTypesAsync();
}
