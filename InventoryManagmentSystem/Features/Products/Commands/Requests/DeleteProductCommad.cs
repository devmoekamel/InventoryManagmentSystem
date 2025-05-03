using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Commands.Requests
{
    public class DeleteProductCommad : IRequest<ResultStatus>
    {
        public int ProductId { get; set; }

    }
}
