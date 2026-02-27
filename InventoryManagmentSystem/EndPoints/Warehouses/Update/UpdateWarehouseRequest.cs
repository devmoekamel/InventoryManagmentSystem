using FluentValidation;

namespace InventoryManagmentSystem.EndPoints.Warehouses.Update;

public class UpdateWarehouseRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UpdateWarehouseRequestValidator : AbstractValidator<UpdateWarehouseRequest>
{
    public UpdateWarehouseRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Valid warehouse ID is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Warehouse name is required");
    }
}
