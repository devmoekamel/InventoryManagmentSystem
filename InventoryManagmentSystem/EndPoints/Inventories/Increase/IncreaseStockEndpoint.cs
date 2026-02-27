using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Inventories.Commands.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Inventories.Increase;

public class IncreaseStockEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/inventories/increase", async (IMediator mediator, IncreaseStockRequest request, CancellationToken ct) =>
        {
            var result = await mediator.Send(new IncreaseProductStockCommand 
            { 
                ProductId = request.ProductId, 
                WarehouseId = request.WarehouseId, 
                Stock = request.Stock 
            }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ResultStatus>.Failure(result.Message));
            }
            return Response(RequestResult<ResultStatus>.Success(result, "Stock increased successfully"));
        })
        .AddEndpointFilter<ValidationFilter<IncreaseStockRequest>>();
    }
}
