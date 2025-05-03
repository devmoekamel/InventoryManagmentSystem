using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using MediatR;

namespace InventoryManagmentSystem.Features.Warehouses.Commands
{
    public class AddWarehouseCommand:IRequest<ResultStatus>
    {
        public string warehouseName;
        public string userid;
    }

    public class AddWarehouseCommandHandler : IRequestHandler<AddWarehouseCommand, ResultStatus>
    {
        private readonly IGenericRepository<Warehouse> warehouseRepo;

        public AddWarehouseCommandHandler(IGenericRepository<Warehouse> warehouseRepo)
        {
            this.warehouseRepo = warehouseRepo;
        }

        public async Task<ResultStatus> Handle(AddWarehouseCommand request, CancellationToken cancellationToken)
        {
            warehouseRepo.Add(new Warehouse
            {
                Name = request.warehouseName,
                CreatedBy = request.userid
            });

           var changes =  await warehouseRepo.SaveChangesAsync();
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
                Message = "Warehouse Created"
            };
        }
    }
}
