using FluentValidation;

namespace InventoryManagmentSystem.EndPoints.Inventories.Transfer;

public class TransferStockRequest
{
    public int ProductId { get; set; }
    public int FromWarehouseId { get; set; }
    public int ToWarehouseId { get; set; }
    public int Stock { get; set; }
}

public class TransferStockRequestValidator : AbstractValidator<TransferStockRequest>
{
    public TransferStockRequestValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Valid product ID is required");
        RuleFor(x => x.FromWarehouseId).GreaterThan(0).WithMessage("Valid source warehouse ID is required");
        RuleFor(x => x.ToWarehouseId).GreaterThan(0).WithMessage("Valid destination warehouse ID is required");
        RuleFor(x => x.Stock).GreaterThan(0).WithMessage("Stock must be greater than 0");
        RuleFor(x => x).Must(x => x.FromWarehouseId != x.ToWarehouseId)
            .WithMessage("Source and destination warehouses must be different");
    }
}
