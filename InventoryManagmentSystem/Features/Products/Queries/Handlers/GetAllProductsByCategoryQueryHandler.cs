using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Core.Services;
using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Features.Products.Queries.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries.Handlers
{

    public class GetAllProductsByCategoryQueryHandler : IRequestHandler<GetAllProductsByCategoryQuery, IEnumerable<ProductDTO>>
    {
        private readonly IGenericRepository<Product> productRepo;

        public GetAllProductsByCategoryQueryHandler(IGenericRepository<Product> ProductRepo)
        {
            productRepo = ProductRepo;
        }
        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = productRepo.GetAll()
                         .Where(p => p.CategoryId == request.CategoryId)
                         .ProjectTo<ProductDTO>()
                         .ToList();

            return products;

        }
    }
}
