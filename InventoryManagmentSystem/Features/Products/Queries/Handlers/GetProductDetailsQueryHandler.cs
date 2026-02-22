using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Core.Services;
using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Features.Products.Queries.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries.Handlers
{
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

            if (product is null)
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
                Data = product,
                ErrorCode = ErrorCode.None
            };

        }
    }
}
