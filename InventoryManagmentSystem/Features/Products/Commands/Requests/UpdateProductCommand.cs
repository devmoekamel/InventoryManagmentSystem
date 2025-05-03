using InventoryClassLibrary.DTO.Products;
using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Commands.Requests
{
    public class UpdateProductCommand : IRequest<ResultStatus>
    {
        public int OldProductId;
        public ProductCreateUpdateDTO NewProductData;
    }

}
