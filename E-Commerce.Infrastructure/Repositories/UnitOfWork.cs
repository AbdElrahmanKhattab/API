using System.Collections;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Data;

namespace E_Commerce.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreDbContext _context;
    private readonly Hashtable _repositories = new();

    public UnitOfWork(StoreDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repository = new GenericRepository<TEntity>(_context);
            _repositories.Add(type, repository);
        }

        return (IGenericRepository<TEntity>)_repositories[type]!;
    }

    public Task<int> CompleteAsync()
    {
        return _context.SaveChangesAsync();
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}
