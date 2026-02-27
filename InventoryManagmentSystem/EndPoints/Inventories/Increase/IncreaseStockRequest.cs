using FluentValidation;

namespace InventoryManagmentSystem.EndPoints.Inventories.Increase;

public class IncreaseStockRequest
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Stock { get; set; }
}

public class IncreaseStockRequestValidator : AbstractValidator<IncreaseStockRequest>
{
    public IncreaseStockRequestValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Valid product ID is required");
        RuleFor(x => x.WarehouseId).GreaterThan(0).WithMessage("Valid warehouse ID is required");
        RuleFor(x => x.Stock).GreaterThan(0).WithMessage("Stock must be greater than 0");
    }
}
