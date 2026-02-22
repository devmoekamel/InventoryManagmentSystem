using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Core.Services;
using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Features.Products.Queries.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries.Handlers
{

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IGenericRepository<Product> _productRepo;

        public GetAllProductsQueryHandler(IGenericRepository<Product> ProductRepo)
        {
            _productRepo = ProductRepo;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return _productRepo.GetAll().ProjectTo<ProductDTO>().ToList();
        }
    }
}
