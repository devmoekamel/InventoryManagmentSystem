using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Inventories.Commands.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Inventories.Decrease;

public class DecreaseStockEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/inventories/decrease", async (IMediator mediator, DecreaseStockRequest request, CancellationToken ct) =>
        {
            var result = await mediator.Send(new DecreaseProductStockCommand 
            { 
                ProductId = request.ProductId, 
                WarehouseId = request.WarehouseId, 
                Stock = request.Stock 
            }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ResultStatus>.Failure(result.Message));
            }
            return Response(RequestResult<ResultStatus>.Success(result, "Stock decreased successfully"));
        })
        .AddEndpointFilter<ValidationFilter<DecreaseStockRequest>>();
    }
}
