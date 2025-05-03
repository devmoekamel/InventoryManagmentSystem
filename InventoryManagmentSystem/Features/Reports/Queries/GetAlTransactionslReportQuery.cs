using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Reports;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagmentSystem.Features.Reports.Queries
{
    public class GetAlTransactionslReportQuery : IRequest<ResultStatus>
    {
        public ReportParamDTO reportParamDTO;
    }

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
                .ToList();


            return new ResultStatus
            {
                Data= Transactions,
                Status=true
            };
        }
    }
}
