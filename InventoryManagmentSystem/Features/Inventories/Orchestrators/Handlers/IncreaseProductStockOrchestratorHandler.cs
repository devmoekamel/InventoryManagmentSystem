using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Enums;
using InventoryManagmentSystem.Features.Inventories.Commands.Requests;
using InventoryManagmentSystem.Features.Inventories.Orchestrators.Requests;
using InventoryManagmentSystem.Features.Transactions.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Orchestrators.Handlers
{
    public class IncreaseProductStockOrchestratorHandler : IRequestHandler<IncreaseProductStockOrchestrator, ResultStatus>
    {
        private readonly IMediator mediator;


        public IncreaseProductStockOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ResultStatus> Handle(IncreaseProductStockOrchestrator request, CancellationToken cancellationToken)
        {

            ResultStatus IncreaseResult = await mediator.Send(new IncreaseProductStockCommand
            {
                ProductId = request.ProductId,
                WarehouseId = request.WarehouseId,
                Stock = request.Stock
            });

            if (!IncreaseResult.Status)
            {
                return IncreaseResult;
            }
            ResultStatus TransactionResult = await mediator.Send(new AddTransactionCommand
            {
                ProductId = request.ProductId,
                ToWarehouseId = request.ProductId,
                Stock = request.Stock,
                TransactionType = TransactionType.Increase,
                UserId = request.UserId
            });

            if (!TransactionResult.Status)
            {
                return TransactionResult;
            }

            return IncreaseResult;
        }
    }
}
