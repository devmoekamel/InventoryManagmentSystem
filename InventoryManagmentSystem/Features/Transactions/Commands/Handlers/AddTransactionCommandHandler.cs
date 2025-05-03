using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Transactions.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Transactions.Commands.Handlers
{
    public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, ResultStatus>
    {
        private readonly IGenericRepository<InventoryTransaction> transactionRepo;

        public AddTransactionCommandHandler(IGenericRepository<InventoryTransaction> TransactionRepo)
        {
            transactionRepo = TransactionRepo;
        }

        public async Task<ResultStatus> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            transactionRepo.Add(new InventoryTransaction
            {
                ProductId = request.ProductId,
                FromWarehouseId = request.FromWarehouseId ?? null,
                ToWarehouseId = request.ToWarehouseId ?? null,
                Stock = request.Stock,
                TransactionType = request.TransactionType,
                CreatedBy = request.UserId
            });


            var changes = await transactionRepo.SaveChangesAsync();

            if (changes <= 0)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "Some thing Went Wrong Try again"
                };
            }

            return new ResultStatus
            {
                Status = true,
                Message = "Done"
            };

        }
    }
}
