using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Inventories.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Commands.Handlrers
{

    public class DecreaseProductStockCommandHandler : IRequestHandler<DecreaseProductStockCommand, ResultStatus>
    {
        private readonly IGenericRepository<Inventory> _inventoryRepo;
        private readonly IMediator mediator;

        public DecreaseProductStockCommandHandler(
            IGenericRepository<Inventory> inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

        public async Task<ResultStatus> Handle(DecreaseProductStockCommand request, CancellationToken cancellationToken)
        {

            var Existinginventory = _inventoryRepo
             .Get(i => i.ProductId == request.ProductId && i.WarehouseId == request.WarehouseId)
             .FirstOrDefault();

            if (Existinginventory is null || Existinginventory.Stock < request.Stock)
            {

                return new ResultStatus
                {
                    Status = false,
                    Message = "Insufficient Product Stock"
                };
            }

            Existinginventory.Stock -= request.Stock;
            _inventoryRepo.UpdateByEntity(Existinginventory);

            var changes = await _inventoryRepo.SaveChangesAsync();

            if (changes <= 0)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "Product Stock didn't  to Warehouse"
                };
            }
            return new ResultStatus
            {
                Status = true,
                Data = Existinginventory.ProductId,
                Message = "Product Stock Decreased  from Warehouse"
            };
        }


    }
}
