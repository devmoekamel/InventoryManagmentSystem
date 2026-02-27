using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Products.Commands.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Products.Update;

public class UpdateProductEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async (IMediator mediator, int id, UpdateProductRequest request, CancellationToken ct) =>
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
            var result = await mediator.Send(new UpdateProductCommand { OldProductId = id, NewProductData = productDto }, ct);
            if (!result.Status)
            {
                return Response(RequestResult<ResultStatus>.Failure(result.Message));
            }
            return Response(RequestResult<ResultStatus>.Success(result, "Product updated successfully"));
        })
        .AddEndpointFilter<ValidationFilter<UpdateProductRequest>>();
    }
}
