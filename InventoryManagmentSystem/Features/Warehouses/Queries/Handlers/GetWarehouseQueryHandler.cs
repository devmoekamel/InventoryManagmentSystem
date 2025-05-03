using InventoryClassLibrary.DTO.Warehouses;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using InventoryManagmentSystem.Features.Warehouses.Queries.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Warehouses.Queries.Handlers
{

    public class GetWarehouseQueryHandler
        : IRequestHandler<GetWarehouseQuery, WarehouseDTO>
    {
        private readonly IGenericRepository<Warehouse> _warehouseRepo;

        public GetWarehouseQueryHandler(IGenericRepository<Warehouse> warehouseRepo)
        {
            _warehouseRepo = warehouseRepo;
        }

        public async Task<WarehouseDTO> Handle(GetWarehouseQuery request, CancellationToken ct)
        {
            var warehouse = _warehouseRepo.GetByID(request.WarehouseId);

            return warehouse.Map<WarehouseDTO>();

        }
    }
}
