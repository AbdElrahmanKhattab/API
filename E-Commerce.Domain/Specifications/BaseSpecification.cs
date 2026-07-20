using System.Linq.Expressions;

namespace E_Commerce.Domain.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria = null) : ISpecification<T>
{
    private readonly List<Expression<Func<T, object>>> includes = [];
    public Expression<Func<T, bool>>? Criteria { get; } = criteria;
    public IReadOnlyList<Expression<Func<T, object>>> Includes => includes;
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public int Skip { get; private set; }
    public int Take { get; private set; }
    public bool IsPagingEnabled { get; private set; }
    protected void AddInclude(Expression<Func<T, object>> expression) => includes.Add(expression);
    protected void AddOrderBy(Expression<Func<T, object>> expression) => OrderBy = expression;
    protected void AddOrderByDescending(Expression<Func<T, object>> expression) => OrderByDescending = expression;
    protected void ApplyPaging(int skip, int take) { Skip = skip; Take = take; IsPagingEnabled = true; }
}
