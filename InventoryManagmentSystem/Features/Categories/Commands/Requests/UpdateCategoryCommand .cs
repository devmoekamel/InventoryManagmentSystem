using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Categories.Commands.Requests
{
    public class UpdateCategoryCommand : IRequest<ResultStatus>
    {
        public int categoryId;
    }
}
