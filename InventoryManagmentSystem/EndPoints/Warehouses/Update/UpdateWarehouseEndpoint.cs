using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Warehouses.Commands.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Warehouses.Update;

public class UpdateWarehouseEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("/warehouses/{id}", async (IMediator mediator, int id, UpdateWarehouseRequest request, CancellationToken ct) =>
        {
            var result = await mediator.Send(new UpdateWarehouseCommand { warehouseId = id }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ResultStatus>.Failure(result.Message));
            }
            return Response(RequestResult<ResultStatus>.Success(result, "Warehouse updated successfully"));
        })
        .AddEndpointFilter<ValidationFilter<UpdateWarehouseRequest>>();
    }
}
