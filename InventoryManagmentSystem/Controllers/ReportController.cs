using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Reports;
using InventoryClassLibrary.Enums;
using InventoryManagmentSystem.Features.Reports.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("low-stock")]

        public async Task<ActionResult> GetlowstockProductsReport([FromQuery] ReportParamDTO reportParam)
        {
          ResultStatus  result =  await mediator.Send(new GetALLLowtStockProductQuery
          { reportParamDTO=reportParam});
           
            if(!result.Status)
            {
                return Ok(ResponseDTO<object>.Error(errorCode:ErrorCode.UnexpectedError));
            }
            
                return Ok(ResponseDTO<object>.Success(data:result.Data));

        }


        [HttpGet("transactions")]

        public async Task<ActionResult> GetTransactionsReport([FromQuery] ReportParamDTO reportParam)
        {
            ResultStatus result = await mediator.Send(new GetAlTransactionslReportQuery { reportParamDTO = reportParam });

            if (!result.Status)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.UnexpectedError));
            }

            return Ok(ResponseDTO<object>.Success(data: result.Data)); 

        }
    }
}
