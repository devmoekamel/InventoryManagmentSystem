using InventoryManagmentSystem.Core.DTO.Warehouses;
using MediatR;

namespace InventoryManagmentSystem.Features.Warehouses.Queries.Requests
{
    public class GetWarehouseQuery : IRequest<WarehouseDTO>
    {
        public int WarehouseId { get; set; }
    }
}
