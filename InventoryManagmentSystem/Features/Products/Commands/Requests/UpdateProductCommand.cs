using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Core.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Commands.Requests
{
    public class UpdateProductCommand : IRequest<ResultStatus>
    {
        public int OldProductId;
        public ProductCreateUpdateDTO NewProductData;
    }

}
