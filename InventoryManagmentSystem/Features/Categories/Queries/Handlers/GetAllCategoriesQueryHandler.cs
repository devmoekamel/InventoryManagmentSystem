using InventoryManagmentSystem.Core.DTO.Categories;
using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Features.Categories.Queries.Requests;
using MediatR;
using InventoryManagmentSystem.Core.Services;

namespace InventoryManagmentSystem.Features.Categories.Queries.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, ResultStatus>
    {
        private readonly IGenericRepository<Category> categoryRepo;

        public GetAllCategoriesQueryHandler(IGenericRepository<Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public async Task<ResultStatus> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = categoryRepo.GetAll()
                .ProjectTo<CategoryDTO>()
                .ToList();

            return new ResultStatus
            {
                Data = categories,
                Status = true,
                ErrorCode = ErrorCode.None
            };
        }
    }
}
