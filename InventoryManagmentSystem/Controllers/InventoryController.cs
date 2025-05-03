using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Inventories;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Repos;
using InventoryManagmentSystem.Features.Inventories.Commands;
using InventoryManagmentSystem.Features.Inventories.Orchestrators;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]

    public class InventoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public InventoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }



        [HttpPost("IncreaseStock")]

        public async Task<ActionResult> AddProductStock(InventoryDTO inventoryDTO)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (!ModelState.IsValid)
            {
                return Ok(ResponseDTO<InventoryDTO>.Error(
                            errorCode: ErrorCode.ValidationError
                            ));
            }

          ResultStatus  Result =  await mediator.Send(new IncreaseProductStockOrchestrator
            {
                ProductId = inventoryDTO.ProductId,
                Stock = inventoryDTO.Stock,
                WarehouseId = inventoryDTO.WarehouseId,
                UserId=userId
            });
            if (!Result.Status)
            {
                return Ok(ResponseDTO<object>
                  .Error(errorCode: Result.ErrorCode, message:Result.Message));
            }

            return Ok(ResponseDTO<object>
                      .Success(data:Result.Message));

        }


        [HttpPost("DecreaseStock")]
        public async Task<ActionResult> DecreaseProductStock(InventoryDTO inventoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResponseDTO<InventoryDTO>.Error(errorCode: ErrorCode.ValidationError));
            }
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            ResultStatus Result = await mediator.Send(new DecreaseProductStockOrchestrator
            {
                ProductId = inventoryDTO.ProductId,
                Stock = inventoryDTO.Stock,
                WarehouseId = inventoryDTO.WarehouseId,
                UserId=userId
            });

            if (!Result.Status)
            {
                return Ok(ResponseDTO<object>
                  .Error(errorCode: Result.ErrorCode, message: Result.Message));
            }

            return Ok(ResponseDTO<object>
                      .Success(data: Result));

        }

        [HttpPost("TransferStock")]
        public async Task<ActionResult> TransferProductStock(TranactionDTO TranactionDTO)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResponseDTO<InventoryDTO>.Error(errorCode: ErrorCode.ValidationError));
            }
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            ResultStatus Result = await mediator.Send(new 
            TransferProductStockOrchestrator
            {
               TranactionDTO= TranactionDTO,
               UserId = userId

            });

            if (!Result.Status)
            {
                return Ok(ResponseDTO<object>
                  .Error(errorCode: Result.ErrorCode, message: Result.Message));
            }

            return Ok(ResponseDTO<object>
                      .Success(data: Result));

        }
    }
}
