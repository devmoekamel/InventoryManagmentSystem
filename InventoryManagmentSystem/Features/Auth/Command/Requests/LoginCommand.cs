using InventoryManagmentSystem.Core.DTO.Auth;
using MediatR;

namespace InventoryManagmentSystem.Features.Auth.Command.Requests
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public LoginDTO LoginData { get; set; }
    }
}
