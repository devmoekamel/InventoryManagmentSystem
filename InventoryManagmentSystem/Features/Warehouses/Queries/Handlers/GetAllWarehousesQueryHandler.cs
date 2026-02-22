using InventoryManagmentSystem.Core.DTO.Warehouses;
using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Features.Warehouses.Queries.Requests;
using MediatR;
using InventoryManagmentSystem.Core.Services;

namespace InventoryManagmentSystem.Features.Warehouses.Queries.Handlers
{

    public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, ResultStatus>
    {
        private readonly IGenericRepository<Warehouse> _warehouseRepo;

        public GetAllWarehousesQueryHandler(IGenericRepository<Warehouse> WarehouseRepo)
        {
            this._warehouseRepo = WarehouseRepo;
        }

        public async Task<ResultStatus> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            var warehouses = _warehouseRepo.GetAll()
                 .ProjectTo<WarehouseDTO>()
                 .ToList();


            return new ResultStatus
            {
                Data = warehouses,
                Status = true,
                ErrorCode = ErrorCode.None
            };
        }
    }
}
