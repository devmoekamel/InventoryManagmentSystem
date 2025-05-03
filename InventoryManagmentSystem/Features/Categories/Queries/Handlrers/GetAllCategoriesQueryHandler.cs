using InventoryClassLibrary.DTO.Categories;
using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Categories.Queries.Requests;
using MediatR;
using InventoryClassLibrary.Services;

namespace InventoryManagmentSystem.Features.Categories.Queries.Handlrers
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
