using InventoryClassLibrary.DTO.Inventories;
using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Commands.Requests
{
    public class TransferProductStockCommand : IRequest<ResultStatus>
    {
        public TranactionDTO TranactionDTO;
    }

}
