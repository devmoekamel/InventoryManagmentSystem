using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.DTO.Auth;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Features.Auth.Command.Requests;
using InventoryManagmentSystem.Middlewares;
using InventoryManagmentSystem.Shared.Models;
using MediatR;

namespace InventoryManagmentSystem.EndPoints.Auth.Register;

public class RegisterEndpoint : EndpointDefinition
{
    public override void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (IMediator mediator, RegisterRequest request, CancellationToken ct) =>
        {
            var registerDto = new RegisterDTO
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password
            };
            var result = await mediator.Send(new RegisterCommand { RegisterData = registerDto }, ct);
            return Response(RequestResult<ResultStatus>.Success(result, "Registration successful"));
        })
        .AddEndpointFilter<ValidationFilter<RegisterRequest>>()
        .AllowAnonymous();
    }
}
