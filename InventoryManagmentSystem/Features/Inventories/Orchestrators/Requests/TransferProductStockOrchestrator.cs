using InventoryClassLibrary.DTO.Inventories;
using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Orchestrators.Requests
{
    public class TransferProductStockOrchestrator : IRequest<ResultStatus>
    {
        public TranactionDTO TranactionDTO;
        public string UserId;

    }
}
