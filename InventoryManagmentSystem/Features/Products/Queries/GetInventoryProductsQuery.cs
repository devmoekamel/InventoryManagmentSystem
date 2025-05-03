using InventoryClassLibrary.DTO.Inventories;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries
{
    public class GetInventoryProductsQuery :IRequest<IEnumerable<ProductInventoryDTO>>
    {
    }

    public class GetInventoryProductsQueryHandler : IRequestHandler<GetInventoryProductsQuery, IEnumerable<ProductInventoryDTO>>
    {
        private readonly IGenericRepository<Inventory> repository;

        public GetInventoryProductsQueryHandler(IGenericRepository<Inventory> repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<ProductInventoryDTO>> Handle(GetInventoryProductsQuery request, CancellationToken cancellationToken)
        {
            return   repository.GetAll()
                     .Where(e => e.Product.IsDeleted==false && e.Warehouse.IsDeleted==false)
                     .ProjectTo<ProductInventoryDTO>()
                     .ToList();
        }
    }
}
