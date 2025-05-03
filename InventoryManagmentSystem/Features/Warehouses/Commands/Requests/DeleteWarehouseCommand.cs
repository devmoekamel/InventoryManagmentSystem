using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Warehouses.Commands.Requests
{
    public class DeleteWarehouseCommand : IRequest<ResultStatus>
    {
        public int warehouseId;
    }
}
