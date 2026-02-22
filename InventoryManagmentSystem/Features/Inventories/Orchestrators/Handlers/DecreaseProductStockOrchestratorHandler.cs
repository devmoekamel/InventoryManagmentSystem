using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Features.Inventories.Commands.Requests;
using InventoryManagmentSystem.Features.Inventories.Orchestrators.Requests;
using InventoryManagmentSystem.Features.Products.Queries.Requests;
using InventoryManagmentSystem.Features.Transactions.Commands.Requests;
using MediatR;
using InventoryManagmentSystem.Core.Services;
using InventoryManagmentSystem.Features.Products.Commands.Requests;

namespace InventoryManagmentSystem.Features.Inventories.Orchestrators.Handlers
{


    public class DecreaseProductStockOrchestratorHandler : IRequestHandler<DecreaseProductStockOrchestrator, ResultStatus>
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IMediator mediator;

        public DecreaseProductStockOrchestratorHandler(IGenericRepository<Product> productRepo, IMediator mediator)
        {
            this.productRepo = productRepo;
            this.mediator = mediator;
        }
        public async Task<ResultStatus> Handle(DecreaseProductStockOrchestrator request, CancellationToken cancellationToken)
        {
            ResultStatus Decreasestatus = await mediator.Send(new DecreaseProductStockCommand
            {
                ProductId = request.ProductId,
                WarehouseId = request.WarehouseId,
                Stock = request.Stock,
            });

            int productId = Decreasestatus.Data;
            var product = productRepo.GetByID(productId);


            product.Quantity -= request.Stock;

            ResultStatus UpdateStatus = await mediator.Send(new UpdateProductCommand
            {
                OldProductId = request.ProductId,
                NewProductData = product.Map<ProductCreateUpdateDTO>()
            });

            if (!UpdateStatus.Status)
            {
                return UpdateStatus;
            }
            if (product.Quantity < product.LowStockThreshold)
            {
                // TODO: Add LowStockNotification handling
            }
            ResultStatus TransactionStatus = await mediator.Send(new AddTransactionCommand
            {
                Stock = request.Stock,
                FromWarehouseId = request.WarehouseId,
                TransactionType = TransactionType.Decrease,
                ProductId = request.ProductId,
                UserId = request.UserId
            });

            if (!TransactionStatus.Status)
            {
                return TransactionStatus;
            }
            return Decreasestatus;
        }
    }
}
