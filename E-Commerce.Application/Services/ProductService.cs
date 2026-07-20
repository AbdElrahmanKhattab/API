using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Services;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async Task<Pagination<ProductDto>> GetProductsAsync(ProductSpecParams parameters)
    {
        var repository = unitOfWork.GetRepository<Product, int>();
        var products = await repository.GetAllWithSpecAsync(new ProductsWithTypesAndBrandsSpecification(parameters));
        var count = await repository.CountAsync(new ProductFilterForCountSpecification(parameters));
        return new Pagination<ProductDto>(parameters.PageIndex, parameters.PageSize, count, mapper.Map<IReadOnlyList<ProductDto>>(products));
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await unitOfWork.GetRepository<Product, int>().GetEntityWithSpecAsync(new ProductsWithTypesAndBrandsSpecification(id));
        return product is null ? null : mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync() => mapper.Map<IEnumerable<BrandDto>>(await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
    public async Task<IEnumerable<TypeDto>> GetAllTypesAsync() => mapper.Map<IEnumerable<TypeDto>>(await unitOfWork.GetRepository<ProductType, int>().GetAllAsync());
}
