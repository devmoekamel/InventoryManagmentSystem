using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Features.Inventories.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Commands.Handlers
{
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
             .Get(i => i.ProductId == request.TransactionDTO.ProductId
             && i.WarehouseId == request.TransactionDTO.FromWarehouseId)
             .FirstOrDefault();

            if (fromInventory is null ||
                fromInventory.Stock < request.TransactionDTO.Stock)
            {
                return new ResultStatus
                {

                    ErrorCode = ErrorCode.InsufficientProductStock,
                    Message = "Insufficient Product Stock",
                    Status = false
                };
            }

            var toInventory = _invwentoryRepo
             .Get(i => i.ProductId == request.TransactionDTO.ProductId
             && i.WarehouseId == request.TransactionDTO.TOWarehouseId)
             .FirstOrDefault();

            fromInventory.Stock -= request.TransactionDTO.Stock;

            _invwentoryRepo.UpdateByEntity(fromInventory);

            if (toInventory is null)
            {
                _invwentoryRepo.Add(new Inventory
                {
                    ProductId = request.TransactionDTO.ProductId,
                    WarehouseId = request.TransactionDTO.TOWarehouseId,
                    Stock = request.TransactionDTO.Stock
                });
            }

            toInventory.Stock += request.TransactionDTO.Stock;
            _invwentoryRepo.UpdateByEntity(toInventory);

            var changes = await _invwentoryRepo.SaveChangesAsync();

            if (changes <= 0)
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
