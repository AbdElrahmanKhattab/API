using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories;

public class GenericRepository<TEntity, TKey>(StoreDbContext dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public async Task<TEntity?> GetByIdAsync(TKey id) => await dbContext.Set<TEntity>().FindAsync(id);
    public async Task<IEnumerable<TEntity>> GetAllAsync() => await dbContext.Set<TEntity>().ToListAsync();
    public async Task<TEntity?> GetEntityWithSpecAsync(ISpecification<TEntity> specification) => await ApplySpecification(specification).FirstOrDefaultAsync();
    public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification) => await ApplySpecification(specification).ToListAsync();
    public async Task<int> CountAsync(ISpecification<TEntity> specification) => await ApplySpecification(specification).CountAsync();
    public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);
    public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);
    public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);
    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification) => SpecificationEvaluator<TEntity>.GetQuery(dbContext.Set<TEntity>().AsQueryable(), specification);
}
