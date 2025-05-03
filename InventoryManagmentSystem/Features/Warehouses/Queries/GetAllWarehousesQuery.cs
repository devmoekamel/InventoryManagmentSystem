using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Warehouses;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using MediatR;

namespace InventoryManagmentSystem.Features.Warehouses.Queries
{
    public class GetAllWarehousesQuery:IRequest<ResultStatus>
    {

    }
    public class GetAllWarehousesQueryHandler :IRequestHandler<GetAllWarehousesQuery,ResultStatus>
    {
        private readonly IGenericRepository<Warehouse> _warehouseRepo;

        public GetAllWarehousesQueryHandler(IGenericRepository<Warehouse> WarehouseRepo)
        {
            this._warehouseRepo = WarehouseRepo;
        }

        public async Task<ResultStatus> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
           var warehouses=  _warehouseRepo.GetAll()
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
