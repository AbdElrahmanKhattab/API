using System.Linq.Expressions;

namespace E_Commerce.Domain.Specifications;

public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
    List<Expression<Func<TEntity, object>>> Includes { get; }
    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDesc { get; }
    int Skip { get; }
    int Take { get; }
    bool IsPagingEnabled { get; }
}
