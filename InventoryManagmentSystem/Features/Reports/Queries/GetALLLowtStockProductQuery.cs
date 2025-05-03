using AutoMapper.QueryableExtensions;
using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Inventories;
using InventoryClassLibrary.DTO.Reports;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using MediatR;

namespace InventoryManagmentSystem.Features.Reports.Queries
{
    public class GetALLLowtStockProductQuery:IRequest<ResultStatus>
    {
        public ReportParamDTO reportParamDTO;

    }

    public class GetALLLowtStockProductQueryHandler:IRequestHandler<GetALLLowtStockProductQuery,ResultStatus>
    {
        private readonly IGenericRepository<Inventory> inventoriesRepo;

        public GetALLLowtStockProductQueryHandler(IGenericRepository<Inventory> InventoriesRepo)
        {
            inventoriesRepo = InventoriesRepo;
        }

        public async Task<ResultStatus> Handle(GetALLLowtStockProductQuery request, CancellationToken cancellationToken)
        {
            var reportdata = request.reportParamDTO;
          var products =  inventoriesRepo.GetAll()
                 .Where(t =>
                    (!reportdata.fromDate.HasValue || t.CreatedAt >= reportdata.fromDate) &&
                    (!reportdata.ToDate.HasValue || t.CreatedAt <= reportdata.ToDate) &&
                    (!reportdata.categoryId.HasValue || t.Product.CategoryId == reportdata.categoryId) &&
                    (!reportdata.ProductId.HasValue || t.ProductId==reportdata.ProductId)
                 ).ProjectTo<ProductInventoryDTO>()
                  .Where(p => p.IsLow == true)
                  .ToList();

            return new ResultStatus
            {
                Data = products,
                Status = true,
            };
        }
    }
}
