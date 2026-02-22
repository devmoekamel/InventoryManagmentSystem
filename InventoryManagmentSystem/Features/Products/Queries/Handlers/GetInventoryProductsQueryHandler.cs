using InventoryManagmentSystem.Core.DTO.Inventories;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Core.Services;
using InventoryManagmentSystem.Features.Products.Queries.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Queries.Handlers
{

    public class GetInventoryProductsQueryHandler : IRequestHandler<GetInventoryProductsQuery, IEnumerable<ProductInventoryDTO>>
    {
        private readonly IGenericRepository<Inventory> repository;

        public GetInventoryProductsQueryHandler(IGenericRepository<Inventory> repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<ProductInventoryDTO>> Handle(GetInventoryProductsQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAll()
                     .Where(e => e.Product.IsDeleted == false && e.Warehouse.IsDeleted == false)
                     .ProjectTo<ProductInventoryDTO>()
                     .ToList();
        }
    }
}
