using InventoryManagmentSystem.DTO.Products;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries.Requests
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }

}
