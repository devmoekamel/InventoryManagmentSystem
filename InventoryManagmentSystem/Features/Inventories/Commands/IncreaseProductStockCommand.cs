using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Products;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using InventoryManagmentSystem.DTO.Products;
using InventoryManagmentSystem.Features.Inventories.Queries;
using InventoryManagmentSystem.Features.Products.Queries;
using InventoryManagmentSystem.Features.Warehouses.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagmentSystem.Features.Inventories.Commands
{
    public class IncreaseProductStockCommand : IRequest<ResultStatus>
    {
        public int ProductId ;
        public int WarehouseId;
        public int Stock;
    }

    public class IncreaseProductStockHandler : IRequestHandler<IncreaseProductStockCommand, ResultStatus>
    {
        private readonly IGenericRepository<Inventory> _inventoryRepo;
        private readonly IMediator mediator;

        public IncreaseProductStockHandler(
            IGenericRepository<Inventory> inventoryRepo,
            IMediator mediator)
        {
            _inventoryRepo = inventoryRepo;
            this.mediator = mediator;
        }

        public async Task<ResultStatus> Handle(IncreaseProductStockCommand request, CancellationToken cancellationToken)
        {
            ;

            var resultStatus = await mediator.Send(new GetProductDetailsQuery { ProductId = request.ProductId });
            ProductDTO  product = resultStatus.Data;
            var warehouse = await mediator.Send(new GetWarehouseQuery { WarehouseId = request.WarehouseId });

            if (warehouse is null || product is null)
            {        return new ResultStatus
                        {
                            Status = false,
                            Message = "no product or  Warehouse with this id"
                        };
            }

            var totalExistingStock =  _inventoryRepo
                .GetAll()
                .Where(i => i.ProductId == product.Id)
                .Sum(i => i.Stock);

            if ((totalExistingStock + request.Stock) > product.Quantity)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "Insufficient Product Stock"
                };
            }


            var existingInventory = _inventoryRepo
          .Get(i => i.ProductId == request.ProductId && i.WarehouseId == request.WarehouseId)
          .FirstOrDefault();
            if (existingInventory is null)
            {
                var newInventory = new Inventory
                {
                    ProductId = request.ProductId,
                    WarehouseId = request.WarehouseId,
                    Stock = request.Stock
                };
                _inventoryRepo.Add(newInventory);

            }

            existingInventory.Stock += request.Stock;
              _inventoryRepo.UpdateByEntity(existingInventory);

           
           var changes =   await _inventoryRepo.SaveChangesAsync();
               
            if(changes<=0)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "Product Stock didn't Increaseed"
                };
            }
            return new ResultStatus
            {
                Status = true,
                Message = "Product Stock  Increaseedd"
            };


        }
    }
}