using FluentValidation;
using InventoryManagmentSystem.Core.DTO.Inventories;

namespace InventoryManagmentSystem.Validators
{
    public class TransactionDTOValidator : AbstractValidator<TransactionDTO>
    {
        public TransactionDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Valid product is required");

            RuleFor(x => x.FromWarehouseId)
                .GreaterThan(0).WithMessage("Source warehouse is required");

            RuleFor(x => x.TOWarehouseId)
                .GreaterThan(0).WithMessage("Destination warehouse is required");

            RuleFor(x => x.Stock)
                .GreaterThan(0).WithMessage("Stock quantity must be greater than 0");

            RuleFor(x => x)
                .Must(x => x.FromWarehouseId != x.TOWarehouseId)
                .WithMessage("Source and destination warehouses must be different");
        }
    }
}
