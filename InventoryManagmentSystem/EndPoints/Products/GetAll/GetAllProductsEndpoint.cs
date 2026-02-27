using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Products.Queries.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Products.GetAll;

public class GetAllProductsEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetAllProductsQuery(), ct);
            return Response(RequestResult<IEnumerable<ProductDTO>>.Success(result, "Products retrieved successfully"));
        });
    }
}
