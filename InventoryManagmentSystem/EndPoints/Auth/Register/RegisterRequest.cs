using FluentValidation;
using InventoryManagmentSystem.Core.DTO.Auth;
using InventoryManagmentSystem.Middlewares;

namespace InventoryManagmentSystem.EndPoints.Auth.Register;

public class RegisterRequest
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}
