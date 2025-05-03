using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Categories.Commands.Requests
{
    public class AddCategoryCommand : IRequest<ResultStatus>
    {
        public string categoryName;
        public string userid;
    }
}
