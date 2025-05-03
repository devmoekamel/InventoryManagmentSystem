using InventoryClassLibrary.DTO.Auth;
using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Auth.Command.Requests
{
    public class RegisterCommand : IRequest<ResultStatus>
    {
        public RegisterDTO RegisterData { get; set; }
    }

}
