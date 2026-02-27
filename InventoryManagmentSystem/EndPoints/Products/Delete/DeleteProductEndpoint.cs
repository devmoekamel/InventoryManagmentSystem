using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Products.Commands.Requests;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Products.Delete;

public class DeleteProductEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
        {
            var result = await mediator.Send(new DeleteProductCommad { ProductId = id }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ResultStatus>.Failure(result.Message));
            }
            return Response(RequestResult<ResultStatus>.Success(result, "Product deleted successfully"));
        });
    }
}
