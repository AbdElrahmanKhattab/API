using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;

namespace E_Commerce.Application.Specifications;

public class ProductFilterForCountSpecification(ProductSpecParams parameters) : BaseSpecification<Product>(product =>
    (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId) &&
    (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId) &&
    (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.Trim().ToLower())));
