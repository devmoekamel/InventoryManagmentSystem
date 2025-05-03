using InventoryClassLibrary.DTO.Inventories;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using InventoryManagmentSystem.Features.Inventories.Queries.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Inventories.Queries.Handlers
{

    public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, InventoryDTO>
    {
        private readonly IGenericRepository<Inventory> inventoryRepo;

        public GetInventoryQueryHandler(IGenericRepository<Inventory> InventoryRepo)
        {
            inventoryRepo = InventoryRepo;
        }

        public async Task<InventoryDTO> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
        {

            var inventory = inventoryRepo
                   .Get(i => i.ProductId == request.ProductId && i.WarehouseId == request.WarehouseId)
                   .Map<InventoryDTO>();

            return inventory;

        }
    }
}
