using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Pagination<ProductDto>> GetProductsAsync(ProductSpecParams specParams)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(specParams);
        var countSpec = new ProductFilterForCountSpecification(specParams);
        var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
        var count = await _unitOfWork.Repository<Product>().CountAsync(countSpec);
        var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);

        return new Pagination<ProductDto>(specParams.PageIndex, specParams.PageSize, count, data);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);
        return product is null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<IReadOnlyList<BrandDto>> GetBrandsAsync()
    {
        var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
        return _mapper.Map<IReadOnlyList<BrandDto>>(brands);
    }

    public async Task<IReadOnlyList<TypeDto>> GetTypesAsync()
    {
        var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
        return _mapper.Map<IReadOnlyList<TypeDto>>(types);
    }
}
