using InventoryManagmentSystem.Core.Data;

namespace InventoryManagmentSystem.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    InventoryContext Context { get; }
    Task<bool> SaveChangesAsync();
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
