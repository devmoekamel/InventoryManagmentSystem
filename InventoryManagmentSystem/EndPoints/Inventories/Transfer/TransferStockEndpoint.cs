using FluentValidation;
using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.DTO.Inventories;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Inventories.Commands.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Inventories.Transfer;

public class TransferStockEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/inventories/transfer", async (IMediator mediator, TransferStockRequest request, CancellationToken ct) =>
        {
            var transactionDto = new TransactionDTO
            {
                ProductId = request.ProductId,
                FromWarehouseId = request.FromWarehouseId,
                TOWarehouseId = request.ToWarehouseId,
                Stock = request.Stock
            };
            var result = await mediator.Send(new TransferProductStockCommand { TransactionDTO = transactionDto }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ResultStatus>.Failure(result.Message));
            }
            return Response(RequestResult<ResultStatus>.Success(result, "Stock transferred successfully"));
        })
        .AddEndpointFilter<ValidationFilter<TransferStockRequest>>();
    }
}
