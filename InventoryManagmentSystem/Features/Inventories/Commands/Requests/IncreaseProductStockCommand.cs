using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Commands.Requests
{
    public class IncreaseProductStockCommand : IRequest<ResultStatus>
    {
        public int ProductId;
        public int WarehouseId;
        public int Stock;
    }
}
