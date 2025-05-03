
using System.Linq.Expressions;
using InventoryClassLibrary.Data;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
namespace InventoryClassLibrary.Repos
{
    public class GeneralRepository<T> :IGenericRepository<T> where T : BaseModel
    {
        InventoryContext _inventoryContext;
        public GeneralRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }

        public void Add(T category)
        {
            _inventoryContext.Set<T>().Add(category);
        }

        public void UpdatebyId(int id, T newentity)
        {
            var entity = GetByID(id);
            if (entity is not null)
            {
                _inventoryContext.Set<T>().Update(newentity);
            }
        }

        public void Remove(int id)
        {
            var entity = GetByID(id);
            if (entity is not null)
            {
                entity.IsDeleted = true;
                UpdateByEntity( entity);
            }
        }

        public IQueryable<T> GetAll()
        {
            return _inventoryContext.Set<T>().
                    Where(e=>e.IsDeleted==false);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _inventoryContext.Set<T>().Where(expression);
        }

        public T GetByID(int id)
        {
           
                return Get(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
        }

        public async Task<int> SaveChangesAsync()
        {
            return  await _inventoryContext.SaveChangesAsync();
        }

        public void UpdateByEntity(T entity)
        {
            _inventoryContext.Set<T>().Update(entity);
        }
    }
}
