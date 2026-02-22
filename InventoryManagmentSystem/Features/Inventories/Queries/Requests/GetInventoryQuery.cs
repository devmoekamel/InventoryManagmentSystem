using InventoryManagmentSystem.Core.DTO.Inventories;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Queries.Requests
{
    public class GetInventoryQuery : IRequest<InventoryDTO>
    {
        public int ProductId;
        public int WarehouseId;
    }
}
