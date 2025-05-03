using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Inventories;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.DTO.Products;
using InventoryManagmentSystem.Features.Inventories.Commands;
using InventoryManagmentSystem.Features.Logs.Notifications.InventoryManagmentSystem.Notifications;
using InventoryManagmentSystem.Features.Products.Queries;
using InventoryManagmentSystem.Features.Transactions.Commands;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace InventoryManagmentSystem.Features.Inventories.Orchestrators
{
    public class TransferProductStockOrchestrator:IRequest<ResultStatus>
    {
        public TranactionDTO TranactionDTO;
        public string UserId;

    }


    public class TransferProductStockOrchestratorHandler : IRequestHandler<TransferProductStockOrchestrator, ResultStatus>
    {
        private readonly IMediator mediator;

        public TransferProductStockOrchestratorHandler(IMediator mediator) 
        {
            this.mediator = mediator;
        }
        public async Task<ResultStatus> Handle(TransferProductStockOrchestrator request, CancellationToken cancellationToken)
        {
            ResultStatus Transferresult = await mediator.Send(new TransferProductStockCommand
            {
                TranactionDTO = request.TranactionDTO
            });


            if (!Transferresult.Status)
            {
                return Transferresult;
            }


            ResultStatus resultStatus = await mediator.Send(new GetProductDetailsQuery
                                        { ProductId=request.TranactionDTO.ProductId});

            ProductDTO product = resultStatus.Data;
            if (product.Quantity < product.LowStockThreshold)
            {
                await mediator.Publish(new LowStockNotification
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    CurrentQuantity = product.Quantity,
                    LowStockThreshold = product.LowStockThreshold,
                    UserId = request.UserId
                });
            }
            ResultStatus Transactionresult = await mediator.Send(new AddTransactionCommand
            {
                ProductId = request.TranactionDTO.ProductId,
                ToWarehouseId = request.TranactionDTO.TOWarehouseId,
                FromWarehouseId=request.TranactionDTO.FromWarehouseId,
                Stock = request.TranactionDTO.Stock,
                TransactionType = TransactionType.Transfer,
                UserId=request.UserId
            });
            if (!Transactionresult.Status)
            {
                return Transactionresult;
            }

            return Transferresult;
        }
    }

}
