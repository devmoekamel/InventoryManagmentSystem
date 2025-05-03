using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Transactions.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Transactions.Commands.Handlers
{

    public class ArchiveTransactionCommandHandlder : IRequestHandler<ArchiveTransactionCommand>
    {
        private readonly IGenericRepository<InventoryTransaction> transactionReepo;

        public ArchiveTransactionCommandHandlder(IGenericRepository<InventoryTransaction> transactionReepo)
        {
            this.transactionReepo = transactionReepo;
        }
        public async Task Handle(ArchiveTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = transactionReepo.GetByID(request.TransactionId);
            if (transaction is null)
            {
                return;
            }
            transaction.IsArchived = true;
            transactionReepo.UpdateByEntity(transaction);

            await transactionReepo.SaveChangesAsync();
        }
    }
}
