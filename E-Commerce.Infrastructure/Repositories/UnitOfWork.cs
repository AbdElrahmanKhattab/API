using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Data;

namespace E_Commerce.Infrastructure.Repositories;

public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
{
    private readonly Dictionary<string, object> repositories = [];

    public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
    {
        var typeName = typeof(TEntity).Name;
        if (repositories.TryGetValue(typeName, out var repository)) return (IGenericRepository<TEntity, TKey>)repository;
        var newRepository = new GenericRepository<TEntity, TKey>(dbContext);
        repositories[typeName] = newRepository;
        return newRepository;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}
