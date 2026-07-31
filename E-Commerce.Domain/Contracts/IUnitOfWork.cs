using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> CompleteAsync();
}
