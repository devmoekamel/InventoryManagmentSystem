using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.DTO.Categories;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Categories.Queries.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Categories.GetAll;

public class GetAllCategoriesEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetAllCategoriesQuery(), ct);
            if (!result.Status)
            {
                return Response(RequestResult<IEnumerable<CategoryDTO>>.Failure(result.Message));
            }
            return Response(RequestResult<IEnumerable<CategoryDTO>>.Success((IEnumerable<CategoryDTO>)result.Data, result.Message));
        });
    }
}
