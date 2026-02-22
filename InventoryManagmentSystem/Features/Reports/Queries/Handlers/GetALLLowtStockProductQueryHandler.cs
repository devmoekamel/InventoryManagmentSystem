using InventoryManagmentSystem.Core.DTO.Inventories;
using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Features.Reports.Queries.Requests;
using MediatR;
using InventoryManagmentSystem.Core.Services;

namespace InventoryManagmentSystem.Features.Reports.Queries.Handlers
{

    public class GetALLLowtStockProductQueryHandler : IRequestHandler<GetALLLowtStockProductQuery, ResultStatus>
    {
        private readonly IGenericRepository<Inventory> inventoriesRepo;

        public GetALLLowtStockProductQueryHandler(IGenericRepository<Inventory> InventoriesRepo)
        {
            inventoriesRepo = InventoriesRepo;
        }

        public async Task<ResultStatus> Handle(GetALLLowtStockProductQuery request, CancellationToken cancellationToken)
        {
            var reportdata = request.reportParamDTO;
            var products = inventoriesRepo.GetAll()
                   .Where(t =>
                      (!reportdata.fromDate.HasValue || t.CreatedAt >= reportdata.fromDate) &&
                      (!reportdata.ToDate.HasValue || t.CreatedAt <= reportdata.ToDate) &&
                      (!reportdata.categoryId.HasValue || t.Product.CategoryId == reportdata.categoryId) &&
                      (!reportdata.ProductId.HasValue || t.ProductId == reportdata.ProductId)
                   )
                   .ProjectTo<ProductInventoryDTO>()
                    .Where(p => p.IsLow == true)
                    .Skip((reportdata.Page - 1) * reportdata.PageSize)
                     .Take(reportdata.PageSize)
                    .ToList();

            return new ResultStatus
            {
                Data = products,
                Status = true,
            };
        }
    }
}
