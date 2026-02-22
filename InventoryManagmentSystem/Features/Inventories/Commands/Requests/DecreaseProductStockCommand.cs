using InventoryManagmentSystem.Core.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Commands.Requests
{
    public class DecreaseProductStockCommand : IRequest<ResultStatus>
    {
        public int ProductId;
        public int WarehouseId;
        public int Stock;
    }
}
