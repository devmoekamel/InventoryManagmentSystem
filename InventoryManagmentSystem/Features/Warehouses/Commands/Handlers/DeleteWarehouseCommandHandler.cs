using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Features.Warehouses.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Warehouses.Commands.Handlers
{

    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, ResultStatus>
    {
        private readonly IGenericRepository<Warehouse> warehouseRepo;

        public DeleteWarehouseCommandHandler(IGenericRepository<Warehouse> warehouseRepo)
        {
            this.warehouseRepo = warehouseRepo;
        }

        public async Task<ResultStatus> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
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
            warehouseRepo.Remove(id: warehouse.Id);

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
                Message = "Warehouse deleted"
            };
        }

    }
}
