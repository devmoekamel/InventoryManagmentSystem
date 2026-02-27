using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Warehouses.Commands.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Warehouses.Create;

public class CreateWarehouseEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/warehouses", async (IMediator mediator, CreateWarehouseRequest request, CancellationToken ct) =>
        {
            var result = await mediator.Send(new AddWarehouseCommand { warehouseName = request.Name }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ResultStatus>.Failure(result.Message));
            }
            return Response(RequestResult<ResultStatus>.Success(result, "Warehouse created successfully"));
        })
        .AddEndpointFilter<ValidationFilter<CreateWarehouseRequest>>();
    }
}
