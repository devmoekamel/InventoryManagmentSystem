using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Inventories;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Commands
{
    public class TransferProductStockCommand:IRequest<ResultStatus>
    {
        public TranactionDTO TranactionDTO;
    }

    public class TransferProductStockCommandHandler : IRequestHandler<TransferProductStockCommand, ResultStatus>
    {
        private readonly IGenericRepository<Inventory> _invwentoryRepo;

        public TransferProductStockCommandHandler(IGenericRepository<Inventory> InvwentoryRepo)
        {
            _invwentoryRepo = InvwentoryRepo;
        }
        public async Task<ResultStatus> Handle(TransferProductStockCommand request, CancellationToken cancellationToken)
        {
            var fromInventory = _invwentoryRepo
             .Get(i => i.ProductId == request.TranactionDTO.ProductId
             && i.WarehouseId == request.TranactionDTO.FromWarehouseId)
             .FirstOrDefault();
            
            if (fromInventory is null ||
                fromInventory.Stock < request.TranactionDTO.Stock)
            {
                return new ResultStatus
                {
                    
                    ErrorCode= ErrorCode.InsufficientProductStock,
                    Message = "Insufficient Product Stock",
                    Status = false
                };
            }
            
            var toInventory = _invwentoryRepo
             .Get(i => i.ProductId == request.TranactionDTO.ProductId
             && i.WarehouseId == request.TranactionDTO.TOWarehouseId)
             .FirstOrDefault();

            fromInventory.Stock -= request.TranactionDTO.Stock;

            _invwentoryRepo.UpdateByEntity( fromInventory);

            if(toInventory is null)
            {
                _invwentoryRepo.Add(new Inventory
                {
                    ProductId = request.TranactionDTO.ProductId,
                    WarehouseId = request.TranactionDTO.TOWarehouseId,
                    Stock = request.TranactionDTO.Stock
                });
            }

            toInventory.Stock += request.TranactionDTO.Stock;
            _invwentoryRepo.UpdateByEntity(toInventory);

            var changes =   await _invwentoryRepo.SaveChangesAsync();

        if(changes<=0)
          {
                return new ResultStatus
                {
                    ErrorCode = ErrorCode.UnexpectedError,
                    Message = "Product Stock didn't Transfer",
                    Status = false
                };
            }
            return new ResultStatus
            {
                ErrorCode = ErrorCode.None,
                Message = "Product Stock  Transfered success",
                Status = true
            };

        }
    }
}
