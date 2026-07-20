using E_Commerce.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories;

public static class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;
        if (specification.Criteria is not null) query = query.Where(specification.Criteria);
        if (specification.OrderBy is not null) query = query.OrderBy(specification.OrderBy);
        if (specification.OrderByDescending is not null) query = query.OrderByDescending(specification.OrderByDescending);
        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        if (specification.IsPagingEnabled) query = query.Skip(specification.Skip).Take(specification.Take);
        return query;
    }
}
