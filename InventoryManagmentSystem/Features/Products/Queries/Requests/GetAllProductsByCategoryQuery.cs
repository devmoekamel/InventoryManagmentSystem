using InventoryManagmentSystem.Core.DTO.Products;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries.Requests
{
    public class GetAllProductsByCategoryQuery : IRequest<IEnumerable<ProductDTO>>
    {
        public int CategoryId;
    }
}
