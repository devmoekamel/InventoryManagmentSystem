using FluentValidation;

namespace InventoryManagmentSystem.EndPoints.Warehouses.Create;

public class CreateWarehouseRequest
{
    public string Name { get; set; }
}

public class CreateWarehouseRequestValidator : AbstractValidator<CreateWarehouseRequest>
{
    public CreateWarehouseRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Warehouse name is required");
    }
}
