using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Enums;
using MediatR;

namespace InventoryManagmentSystem.Features.Transactions.Commands.Requests
{
    public class AddTransactionCommand : IRequest<ResultStatus>
    {
        public int ProductId { get; set; }
        public int? FromWarehouseId { get; set; }
        public int? ToWarehouseId { get; set; }
        public int Stock { get; set; }
        public TransactionType TransactionType { get; set; }
        public string UserId;

    }
}
