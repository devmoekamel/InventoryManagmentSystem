using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Features.Products.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using InventoryManagmentSystem.Features.Products.Commands.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("All")]
      public async Task<ActionResult> GetAllProducts()
        {
         var Products   = await  mediator.Send(new GetAllProductsQuery());
            return Ok(ResponseDTO<IEnumerable<ProductDTO>>.Success(Products));
        }
        [HttpGet("in-inventory")]
        public async Task<ActionResult> GetAllInventoryProducts()
        {
            var products = await mediator.Send(new GetInventoryProductsQuery());

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetProductDetails(int id)
        {
           var resultStatus = await  mediator.Send(new GetProductDetailsQuery { ProductId=id });
            if (!resultStatus.Status)
            {
                return Ok(ResponseDTO<object>.Error(
                    errorCode: resultStatus.ErrorCode
                    , message: resultStatus.Message));
            }

            return Ok(ResponseDTO<object>
                .Success(
                data: resultStatus.Data));
        }

        [HttpPost]
        public async Task<ActionResult> AddNewProduct(ProductCreateUpdateDTO NewProduct)
        {
            if(!ModelState.IsValid) 
            {
                return Ok(ResponseDTO<object>.Error(
                        errorCode:ErrorCode.ValidationError
                        ,ModelState.ToString()));
            }
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            ResultStatus ResultStatus =  await mediator.Send(new AddProductCommand { 
                NewProductData= NewProduct,
                  UserId=userId});
          
            if(!ResultStatus.Status)
            {
                return Ok(ResponseDTO<object>.Error(
                    errorCode:ResultStatus.ErrorCode
                    ,message:ResultStatus.Message));
            }

            return Ok(ResponseDTO<object>
                .Success(
                data:ResultStatus.Message,
                message:"Product Created"));
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> UpdateProduct(int id,[FromBody]ProductCreateUpdateDTO NewProduct)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResponseDTO<object>.Error(
                          errorCode: ErrorCode.ValidationError
                         ,ModelState.ToString()));
            }

           ResultStatus ResultStatus  = await mediator.Send(new UpdateProductCommand { OldProductId = id, NewProductData = NewProduct });

            if(!ResultStatus.Status)
            {
               return Ok(ResponseDTO<object>.Error(
                   errorCode: ResultStatus.ErrorCode,
                   message:ResultStatus.Message));
            }
               return Ok(ResponseDTO<object>.Success(data:ResultStatus.Message));

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteProduct(int id)
        {

            ResultStatus ResultStatus = await mediator.Send(new DeleteProductCommad { ProductId = id });

            if (!ResultStatus.Status)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ResultStatus.ErrorCode));
            }
            return Ok(ResponseDTO<object>.Success(data:ResultStatus.Message));

        }

    }
}
