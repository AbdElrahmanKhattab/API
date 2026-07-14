using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;

namespace E_Commerce.Application.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams parameters)
        : base(product =>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId) &&
            (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.Trim().ToLower())))
    {
        AddInclude(product => product.Brand);
        AddInclude(product => product.Type);
        ApplyPaging((parameters.PageIndex - 1) * parameters.PageSize, parameters.PageSize);
        switch (parameters.Sort?.ToLower())
        {
            // SQLite cannot order decimal columns directly; casting keeps the database query server-side.
            case "priceasc": AddOrderBy(product => (double)product.Price); break;
            case "pricedesc": AddOrderByDescending(product => (double)product.Price); break;
            case "namedesc": AddOrderByDescending(product => product.Name); break;
            default: AddOrderBy(product => product.Name); break;
        }
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(product => product.Id == id)
    {
        AddInclude(product => product.Brand);
        AddInclude(product => product.Type);
    }
}
