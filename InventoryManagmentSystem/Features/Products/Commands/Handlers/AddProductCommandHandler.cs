using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using InventoryManagmentSystem.Features.Products.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Products.Commands.Handlers
{

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ResultStatus>
    {
        private readonly IGenericRepository<Product> productRepo;

        public AddProductCommandHandler(IGenericRepository<Product> ProductRepo)
        {
            productRepo = ProductRepo;
        }
        public async Task<ResultStatus> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var newproduct = request.NewProductData.Map<Product>();
            newproduct.CreatedBy = request.UserId;

            productRepo.Add(newproduct);

            var changes = await productRepo.SaveChangesAsync();

            if (changes <= 0)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "No Product Created Wrong happend",
                    ErrorCode = ErrorCode.UnexpectedError
                };
            }

            return new ResultStatus
            {
                Status = true,
                Message = " Product Created ",
                ErrorCode = ErrorCode.None
            };


        }


    }
}
