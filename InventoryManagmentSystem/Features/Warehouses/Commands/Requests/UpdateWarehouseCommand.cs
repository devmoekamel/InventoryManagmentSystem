using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Warehouses.Commands.Requests
{
    public class UpdateWarehouseCommand : IRequest<ResultStatus>
    {
        public int warehouseId;
    }
}
