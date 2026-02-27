using InventoryManagmentSystem.Core.Data;

namespace InventoryManagmentSystem.Domain.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public InventoryContext Context { get; }

    public UnitOfWork(InventoryContext context)
    {
        Context = context;
    }

    public void Dispose()
    {
        Context?.Dispose();
    }

    public async Task<bool> SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        await Context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
