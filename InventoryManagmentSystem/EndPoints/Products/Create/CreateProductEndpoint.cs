using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Products.Commands.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Products.Create;

public class CreateProductEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (IMediator mediator, CreateProductRequest request, CancellationToken ct) =>
        {
            var productDto = new ProductCreateUpdateDTO
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                LowStockThreshold = request.LowStockThreshold,
                CategoryId = request.CategoryId
            };
            var result = await mediator.Send(new AddProductCommand { NewProductData = productDto }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ResultStatus>.Failure(result.Message));
            }
            return Response(RequestResult<ResultStatus>.Success(result, "Product created successfully"));
        })
        .AddEndpointFilter<ValidationFilter<CreateProductRequest>>();
    }
}
