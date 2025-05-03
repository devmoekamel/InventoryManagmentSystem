using MediatR;

namespace InventoryManagmentSystem.Features.Transactions.Commands.Requests
{
    public class ArchiveTransactionCommand : IRequest
    {
        public int TransactionId;
    }
}
