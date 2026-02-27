using FluentValidation;

namespace InventoryManagmentSystem.EndPoints.Products.Update;

public class UpdateProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public int LowStockThreshold { get; set; }
    public int CategoryId { get; set; }
}

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Valid product ID is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be 0 or more");
        RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Valid category is required");
    }
}
