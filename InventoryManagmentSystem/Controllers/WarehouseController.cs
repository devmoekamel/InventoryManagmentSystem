using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Warehouses;
using InventoryClassLibrary.Enums;
using InventoryManagmentSystem.Features.Warehouses.Commands.Requests;
using InventoryManagmentSystem.Features.Warehouses.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator mediator;

        public WarehouseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWarehouses() 
        {
         ResultStatus resultStatus   =await  mediator.Send(new GetAllWarehousesQuery());

            return Ok(ResponseDTO<object>.Success(data:resultStatus.Data));
        }


        [HttpPost]
        public async Task<IActionResult> AddWarehouse(WarehouseCreateUpdateDTO warehouseddata)
        {
            if(!ModelState.IsValid)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.ValidationError));
            }
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            ResultStatus result = await mediator.Send(new AddWarehouseCommand { userid = userId, warehouseName = warehouseddata.Name });
              if(!result.Status)
                {
                    return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.UnexpectedError));
                }
            return Ok(ResponseDTO<object>.Success(data:result.Message));

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateWarehouse(int id,WarehouseCreateUpdateDTO warehousedata)
        {
            if(!ModelState.IsValid)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.ValidationError));

            }
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

           ResultStatus result = await mediator.Send(new AddWarehouseCommand
            {
                userid = userId,
                warehouseName = warehousedata.Name 
            });
            if (!result.Status)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.UnexpectedError));
            }
            return Ok(ResponseDTO<object>.Success(data: result.Message));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteWarehouse(int id)
        {
             ResultStatus result  = await  mediator.Send(new DeleteWarehouseCommand { warehouseId = id });

            if (!result.Status)
            {
                return Ok(ResponseDTO<object>.Error(
                    errorCode: ErrorCode.UnexpectedError
                    ,message:result.Message));
            }
            return Ok(ResponseDTO<object>.Success(data: result.Message));
        }

    }
}
