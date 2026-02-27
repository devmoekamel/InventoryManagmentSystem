using InventoryManagmentSystem.Core.DTO.Auth;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Auth.Command.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Auth.Login;

public class LoginEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", async (IMediator mediator, LoginRequest request, CancellationToken ct) =>
        {
            var loginDto = new LoginDTO
            {
                UserName = request.UserName,
                Password = request.Password
            };
            var result = await mediator.Send(new LoginCommand { LoginData = loginDto }, ct);
            return Response(RequestResult<LoginResult>.Success(result, "Login successful"));
        })
        .AddEndpointFilter<ValidationFilter<LoginRequest>>()
        .AllowAnonymous();
    }
}
