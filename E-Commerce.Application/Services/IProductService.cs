using E_Commerce.Application.DTOs;
using E_Commerce.Application.Specifications;

namespace E_Commerce.Application.Services;

public interface IProductService
{
    Task<Pagination<ProductDto>> GetProductsAsync(ProductSpecParams parameters);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    Task<IEnumerable<TypeDto>> GetAllTypesAsync();
}
