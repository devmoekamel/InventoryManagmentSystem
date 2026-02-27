using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Products.Queries.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Products.GetById;

public class GetProductByIdEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetProductDetailsQuery { ProductId = id }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ProductDTO>.Failure(result.Message));
            }
            return Response(RequestResult<ProductDTO>.Success((ProductDTO)result.Data, result.Message));
        });
    }
}
