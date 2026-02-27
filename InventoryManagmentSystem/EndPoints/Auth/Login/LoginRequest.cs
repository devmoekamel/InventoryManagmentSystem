using FluentValidation;
using InventoryManagmentSystem.Core.DTO.Auth;
using InventoryManagmentSystem.Middlewares;

namespace InventoryManagmentSystem.EndPoints.Auth.Login;

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}
