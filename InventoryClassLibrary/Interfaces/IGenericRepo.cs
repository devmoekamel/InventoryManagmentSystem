using InventoryClassLibrary.Models;
using System.Linq.Expressions;

namespace InventoryClassLibrary.Interfaces
{
    public interface IGenericRepository<T>   where T : BaseModel
    {
        void Add(T entity);
        void UpdatebyId(int id ,T entity);
        void UpdateByEntity(T entity);
        void Remove(int id);
        T GetByID(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);

        Task<int> SaveChangesAsync();
    }
}
