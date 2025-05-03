using InventoryClassLibrary.DTO.Products;
using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Commands.Requests
{
    public class AddProductCommand : IRequest<ResultStatus>
    {
        public ProductCreateUpdateDTO NewProductData;
        public string UserId;
    }
}
