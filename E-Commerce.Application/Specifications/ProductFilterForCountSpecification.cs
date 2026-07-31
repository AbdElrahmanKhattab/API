using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;

namespace E_Commerce.Application.Specifications;

public class ProductFilterForCountSpecification : BaseSpecification<Product>
{
    public ProductFilterForCountSpecification(ProductSpecParams specParams)
        : base(product =>
            (!specParams.BrandId.HasValue || product.BrandId == specParams.BrandId.Value)
            && (!specParams.TypeId.HasValue || product.TypeId == specParams.TypeId.Value)
            && (string.IsNullOrWhiteSpace(specParams.Search) || product.Name.ToLower().Contains(specParams.Search)))
    {
    }
}
