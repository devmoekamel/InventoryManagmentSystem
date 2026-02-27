using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Warehouses.Commands.Requests;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Warehouses.Delete;

public class DeleteWarehouseEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/warehouses/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
        {
            var result = await mediator.Send(new DeleteWarehouseCommand { warehouseId = id }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ResultStatus>.Failure(result.Message));
            }
            return Response(RequestResult<ResultStatus>.Success(result, "Warehouse deleted successfully"));
        });
    }
}
