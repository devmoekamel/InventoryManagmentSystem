using InventoryManagmentSystem.Core.DTO.Warehouses;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Warehouses.Queries.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Warehouses.GetById;

public class GetWarehouseByIdEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/warehouses/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetWarehouseQuery { WarehouseId = id }, ct);
            return Response(RequestResult<WarehouseDTO>.Success(result, "Warehouse retrieved successfully"));
        });
    }
}
