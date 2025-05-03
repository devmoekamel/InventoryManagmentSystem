using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Orchestrators.Requests
{
    public class IncreaseProductStockOrchestrator : IRequest<ResultStatus>
    {
        public int ProductId;
        public int WarehouseId;
        public int Stock;
        public string UserId;

    }

}