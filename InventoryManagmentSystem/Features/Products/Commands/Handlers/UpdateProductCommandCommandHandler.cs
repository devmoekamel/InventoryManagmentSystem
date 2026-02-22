using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Core.Services;
using InventoryManagmentSystem.Features.Products.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Commands.Handlers
{

    public class UpdateProductCommandCommandHandler : IRequestHandler<UpdateProductCommand, ResultStatus>
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IMediator mediator;

        public UpdateProductCommandCommandHandler(IGenericRepository<Product> ProductRepo, IMediator mediator)
        {
            productRepo = ProductRepo;
            this.mediator = mediator;
        }
        public async Task<ResultStatus> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = productRepo.GetByID(request.OldProductId);
            if (product is null)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "Product not exist",
                    ErrorCode = ErrorCode.NotFound
                };
            }



            var newproduct = request.NewProductData.Map(product);

            productRepo.UpdateByEntity(newproduct);


            var changes = await productRepo.SaveChangesAsync();

            if (changes <= 0)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "No Product  update",
                    ErrorCode = ErrorCode.UnexpectedError
                };
            }
            return new ResultStatus
            {
                Status = true,
                Message = " Product  updatded",
                ErrorCode = ErrorCode.None
            };

        }
    }
}
