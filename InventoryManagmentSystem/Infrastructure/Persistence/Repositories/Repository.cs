using System.Linq.Expressions;
using InventoryManagmentSystem.Core.Data;
using InventoryManagmentSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagmentSystem.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T>, Core.Interfaces.IGenericRepository<T> where T : BaseModel
{
    private readonly InventoryContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(InventoryContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null)
    {
        var query = _dbSet.Where(entity => !entity.IsDeleted);
        return predicate == null ? query : query.Where(predicate);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // IGenericRepository implementation
    public void Add(T entity) => _dbSet.Add(entity);

    public void UpdatebyId(int id, T entity)
    {
        var existing = _dbSet.Find(id);
        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(entity);
        }
    }

    public void UpdateByEntity(T entity) => _dbSet.Update(entity);

    public void Remove(int id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }
    }

    public T GetByID(int id) => _dbSet.Find(id) ?? throw new KeyNotFoundException();

    public IQueryable<T> GetAll() => _dbSet.Where(e => !e.IsDeleted);
}
