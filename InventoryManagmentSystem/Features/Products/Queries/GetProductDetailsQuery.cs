using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using InventoryManagmentSystem.DTO.Products;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries
{
    public class GetProductDetailsQuery:IRequest<ResultStatus>
    {
        public int ProductId { get; set;}
    }

    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ResultStatus>
    {
        private readonly IGenericRepository<Product> productRepo;
        public GetProductDetailsQueryHandler(IGenericRepository<Product> ProductRepo)
        {
            productRepo = ProductRepo;
        }


        public async Task<ResultStatus> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var product = productRepo.Get(p => p.Id == request.ProductId)
                          .ProjectTo<ProductDTO>()
                          .FirstOrDefault();

            if (product is  null)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "Product not exist",
                    ErrorCode = ErrorCode.NotFound
                };
            }

            return new ResultStatus
            {
                Status = true,
                Data=product,
                ErrorCode = ErrorCode.None
            };

        }
    }
}
