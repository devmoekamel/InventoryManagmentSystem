using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Commands
{
    public class DeleteProductCommad:IRequest<ResultStatus>
    {
        public int ProductId { get; set; }

    }

    public class DeleteProductCommadHandler : IRequestHandler<DeleteProductCommad, ResultStatus>
    {

        private readonly IGenericRepository<Product> _productRepo;

        public DeleteProductCommadHandler(IGenericRepository<Product> ProductRepo)
        {
            _productRepo = ProductRepo;
        }
        public async Task<ResultStatus> Handle(DeleteProductCommad request, CancellationToken cancellationToken)
        {
            var product  = _productRepo.GetByID(request.ProductId);

            if (product is null)
            {
                return new ResultStatus 
                {
                    Status=false,
                    Message="Product doesn't exist"
                };
            }
            _productRepo.Remove(product.Id);

            
            var changes =    await _productRepo.SaveChangesAsync();

            if (changes <= 0)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "No Product Dleteeeed Wrong happend",
                    ErrorCode = ErrorCode.UnexpectedError
                };
            }

            return new ResultStatus
            {
                Status = true,
                Message = " Product deleted ",
                ErrorCode = ErrorCode.None
            };
        }
    }

}
