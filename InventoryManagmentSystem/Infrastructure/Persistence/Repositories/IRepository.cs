using System.Linq.Expressions;

namespace InventoryManagmentSystem.Infrastructure.Persistence.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null);
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> SaveChangesAsync();
}
