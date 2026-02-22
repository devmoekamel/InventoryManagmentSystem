using InventoryManagmentSystem.Core.DTO.Inventories;
using InventoryManagmentSystem.Core.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Commands.Requests
{
    public class TransferProductStockCommand : IRequest<ResultStatus>
    {
        public TransactionDTO TransactionDTO;
    }

}
