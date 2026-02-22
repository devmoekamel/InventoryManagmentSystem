using InventoryManagmentSystem.Core.DTO.Inventories;
using InventoryManagmentSystem.Core.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Orchestrators.Requests
{
    public class TransferProductStockOrchestrator : IRequest<ResultStatus>
    {
        public TransactionDTO TransactionDTO;
        public string UserId;

    }
}
