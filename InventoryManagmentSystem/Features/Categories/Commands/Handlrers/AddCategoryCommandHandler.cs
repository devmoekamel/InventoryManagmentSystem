using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Categories.Commands.Requests;
using MediatR;

namespace InventoryManagmentSystem.Features.Categories.Commands.Handlrers
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, ResultStatus>
    {
        private readonly IGenericRepository<Category> categoryRepo;

        public AddCategoryCommandHandler(IGenericRepository<Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public async Task<ResultStatus> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            categoryRepo.Add(new Category
            {
                Name = request.categoryName,
                CreatedBy = request.userid
            });

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
                Message = "Category Created"
            };
        }
    }
}
