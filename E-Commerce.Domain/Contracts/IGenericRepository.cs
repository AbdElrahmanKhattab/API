using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;

namespace E_Commerce.Domain.Contracts;

public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetEntityWithSpecAsync(ISpecification<TEntity> specification);
    Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification);
    Task<int> CountAsync(ISpecification<TEntity> specification);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
