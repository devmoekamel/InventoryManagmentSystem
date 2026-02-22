using InventoryManagmentSystem.Core.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries.Requests
{
    public class GetProductDetailsQuery : IRequest<ResultStatus>
    {
        public int ProductId { get; set; }
    }
}
