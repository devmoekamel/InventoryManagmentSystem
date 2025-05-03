using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Warehouses.Commands.Requests
{
    public class AddWarehouseCommand : IRequest<ResultStatus>
    {
        public string warehouseName;
        public string userid;
    }

}
