using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;

namespace E_Commerce.Application.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams specParams)
        : base(product =>
            (!specParams.BrandId.HasValue || product.BrandId == specParams.BrandId.Value)
            && (!specParams.TypeId.HasValue || product.TypeId == specParams.TypeId.Value)
            && (string.IsNullOrWhiteSpace(specParams.Search) || product.Name.ToLower().Contains(specParams.Search)))
    {
        AddInclude(product => product.Brand);
        AddInclude(product => product.Type);
        ApplySorting(specParams.Sort);
        ApplyPaging((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
    }

    public ProductsWithTypesAndBrandsSpecification(int id)
        : base(product => product.Id == id)
    {
        AddInclude(product => product.Brand);
        AddInclude(product => product.Type);
    }

    private void ApplySorting(string? sort)
    {
        switch (sort)
        {
            case "priceasc":
                AddOrderBy(product => (double)product.Price);
                break;
            case "pricedesc":
                AddOrderByDesc(product => (double)product.Price);
                break;
            case "namedesc":
                AddOrderByDesc(product => product.Name);
                break;
            default:
                AddOrderBy(product => product.Name);
                break;
        }
    }
}
