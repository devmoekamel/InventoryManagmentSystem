using InventoryClassLibrary.DTO.Reports;
using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Reports.Queries.Requests;
using MediatR;
using InventoryClassLibrary.Services;

namespace InventoryManagmentSystem.Features.Reports.Queries.Handlers
{

    public class GetAlTransactionslReportQueryHandler : IRequestHandler<GetAlTransactionslReportQuery, ResultStatus>
    {
        private readonly IGenericRepository<InventoryTransaction> transactionRepo;

        public GetAlTransactionslReportQueryHandler(IGenericRepository<InventoryTransaction> transactionRepo)
        {
            this.transactionRepo = transactionRepo;
        }
        public async Task<ResultStatus> Handle(GetAlTransactionslReportQuery request, CancellationToken cancellationToken)
        {
            var reportdata = request.reportParamDTO;

            var Transactions = transactionRepo.GetAll()
                .Where(t =>
                    (!reportdata.fromDate.HasValue || t.CreatedAt >= reportdata.fromDate.Value) &&
                    (!reportdata.ToDate.HasValue || t.CreatedAt <= reportdata.ToDate.Value) &&
                    (!reportdata.categoryId.HasValue || t.Product.CategoryId == reportdata.categoryId.Value) &&
                    (!reportdata.transactionType.HasValue || t.TransactionType == reportdata.transactionType.Value)
                ).ProjectTo<TransactionReportDTO>()
                .Skip((reportdata.Page - 1) * reportdata.PageSize)
                 .Take(reportdata.PageSize)
                .ToList();


            return new ResultStatus
            {
                Data = Transactions,
                Status = true
            };
        }
    }
}
