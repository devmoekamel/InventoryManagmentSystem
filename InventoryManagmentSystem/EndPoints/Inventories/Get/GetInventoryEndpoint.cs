using InventoryManagmentSystem.Core.DTO.Inventories;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Inventories.Queries.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Inventories.Get;

public class GetInventoryEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/inventories", async (IMediator mediator, int productId, int warehouseId, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetInventoryQuery { ProductId = productId, WarehouseId = warehouseId }, ct);
            return Response(RequestResult<InventoryDTO>.Success(result, "Inventory retrieved successfully"));
        });
    }
}
