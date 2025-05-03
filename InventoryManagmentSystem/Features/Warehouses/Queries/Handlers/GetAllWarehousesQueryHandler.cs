using InventoryClassLibrary.DTO.Warehouses;
using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Warehouses.Queries.Requests;
using MediatR;
using InventoryClassLibrary.Services;

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
