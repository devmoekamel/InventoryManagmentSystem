using InventoryManagmentSystem.Core.DTO.Inventories;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries.Requests
{
    public class GetInventoryProductsQuery : IRequest<IEnumerable<ProductInventoryDTO>>
    {
    }
}
