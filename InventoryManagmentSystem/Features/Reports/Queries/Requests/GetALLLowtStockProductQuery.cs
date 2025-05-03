using InventoryClassLibrary.DTO.Reports;
using InventoryClassLibrary.DTO;
using MediatR;

namespace InventoryManagmentSystem.Features.Reports.Queries.Requests
{
    public class GetALLLowtStockProductQuery : IRequest<ResultStatus>
    {
        public ReportParamDTO reportParamDTO;

    }
}
