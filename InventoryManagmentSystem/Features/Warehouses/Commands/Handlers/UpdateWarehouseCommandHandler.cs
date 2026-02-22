using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Features.Warehouses.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Warehouses.Commands.Handlers
{

    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, ResultStatus>
    {
        private readonly IGenericRepository<Warehouse> warehouseRepo;

        public UpdateWarehouseCommandHandler(IGenericRepository<Warehouse> warehouseRepo)
        {
            this.warehouseRepo = warehouseRepo;
        }

        public async Task<ResultStatus> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {

            Warehouse warehouse = warehouseRepo.GetByID(request.warehouseId);

            if (warehouse is null)
            {
                return new ResultStatus
                {
                    Message = "warehouse not exist",
                    Status = false
                };
            }

            warehouseRepo.UpdateByEntity(warehouse);
            var changes = await warehouseRepo.SaveChangesAsync();

            if (changes <= 0)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "somthing went wrongh"
                };
            }

            return new ResultStatus
            {
                Status = true,
                Message = "Warehouse updted"
            };
        }
    }
}
