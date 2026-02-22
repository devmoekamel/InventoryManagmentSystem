using InventoryManagmentSystem.Core.DTO.Auth;
using InventoryManagmentSystem.Core.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Auth.Command.Requests
{
    public class RegisterCommand : IRequest<ResultStatus>
    {
        public RegisterDTO RegisterData { get; set; }
    }

}
