using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Categories.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Categories.Commands.Handlrers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResultStatus>
    {
        private readonly IGenericRepository<Category> categoryRepo;

        public DeleteCategoryCommandHandler(IGenericRepository<Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public async Task<ResultStatus> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = categoryRepo.GetByID(request.categoryId);
            if (category is null)
            {
                return new ResultStatus
                {
                    Message = "category not exist",
                    Status = false
                };
            }
            categoryRepo.Remove(id: category.Id);

            var changes = await categoryRepo.SaveChangesAsync();
            if (changes <= 0)
            {
                return new ResultStatus
                {
                    Status = false,
                    Message = "somthing went wrongh"
                };
            }

            return new ResultStatus
            {
                Status = true,
                Message = "Category deleted"
            };
        }
    }
}
