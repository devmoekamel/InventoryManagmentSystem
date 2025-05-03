using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Categories.Commands.Requests
{
    public class DeleteCategoryCommand : IRequest<ResultStatus>
    {
        public int categoryId;
    }
}
