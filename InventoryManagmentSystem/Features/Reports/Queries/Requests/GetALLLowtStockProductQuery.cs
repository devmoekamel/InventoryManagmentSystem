using InventoryManagmentSystem.Core.DTO.Reports;
using InventoryManagmentSystem.Core.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Reports.Queries.Requests
{
    public class GetALLLowtStockProductQuery : IRequest<ResultStatus>
    {
        public ReportParamDTO reportParamDTO;

    }
}
