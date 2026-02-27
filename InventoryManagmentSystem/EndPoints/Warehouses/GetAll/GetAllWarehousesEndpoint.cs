using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.DTO.Warehouses;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Warehouses.Queries.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Warehouses.GetAll;

public class GetAllWarehousesEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/warehouses", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetAllWarehousesQuery(), ct);
            if (!result.Status)
            {
                return Response(RequestResult<IEnumerable<WarehouseDTO>>.Failure(result.Message));
            }
            return Response(RequestResult<IEnumerable<WarehouseDTO>>.Success((IEnumerable<WarehouseDTO>)result.Data, result.Message));
        });
    }
}
