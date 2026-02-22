using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Features.Inventories.Commands.Requests;
using InventoryManagmentSystem.Features.Inventories.Orchestrators.Requests;
using InventoryManagmentSystem.Features.Products.Queries.Requests;
using InventoryManagmentSystem.Features.Transactions.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Orchestrators.Handlers
{

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
                TransactionDTO = request.TransactionDTO
            });


            if (!Transferresult.Status)
            {
                return Transferresult;
            }


            ResultStatus resultStatus = await mediator.Send(new GetProductDetailsQuery
            { ProductId = request.TransactionDTO.ProductId });

            ProductDTO product = resultStatus.Data;
            if (product.Quantity < product.LowStockThreshold)
            {
                // TODO: Add LowStockNotification handling
            }
            ResultStatus Transactionresult = await mediator.Send(new AddTransactionCommand
            {
                ProductId = request.TransactionDTO.ProductId,
                ToWarehouseId = request.TransactionDTO.TOWarehouseId,
                FromWarehouseId = request.TransactionDTO.FromWarehouseId,
                Stock = request.TransactionDTO.Stock,
                TransactionType = TransactionType.Transfer,
                UserId = request.UserId
            });
            if (!Transactionresult.Status)
            {
                return Transactionresult;
            }

            return Transferresult;
        }
    }
}
