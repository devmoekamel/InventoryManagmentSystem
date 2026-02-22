using FluentValidation;
using InventoryManagmentSystem.Core.DTO.Categories;

namespace InventoryManagmentSystem.Validators
{
    public class CategoryCreateUpdateValidator : AbstractValidator<CategoryCreateUpdateDTO>
    {
        public CategoryCreateUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters");
        }
    }
}
