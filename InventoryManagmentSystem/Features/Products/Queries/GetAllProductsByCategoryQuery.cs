using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using InventoryManagmentSystem.DTO.Products;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventoryManagmentSystem.Features.Products.Queries
{
    public class GetAllProductsByCategoryQuery:IRequest<IEnumerable<ProductDTO>>
    {
        public int CategoryId;
    }


    public class GetAllProductsByCategoryQueryHandler : IRequestHandler<GetAllProductsByCategoryQuery, IEnumerable<ProductDTO>>
    {
        private readonly IGenericRepository<Product> productRepo;

        public GetAllProductsByCategoryQueryHandler(IGenericRepository<Product> ProductRepo)
        {
            productRepo = ProductRepo;
        }
        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
           var products =  productRepo.GetAll()
                        .Where(p => p.CategoryId == request.CategoryId)
                        .ProjectTo<ProductDTO>()
                        .ToList();

            return products;
            
        }
    }

}
