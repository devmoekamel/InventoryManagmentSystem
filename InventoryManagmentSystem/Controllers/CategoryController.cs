using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Core.DTO.Categories;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Features.Categories.Commands.Requests;
using InventoryManagmentSystem.Features.Categories.Orchestrators.Requests;
using InventoryManagmentSystem.Features.Categories.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            ResultStatus resultStatus = await mediator.Send(new GetAllCategoriesQuery());
            return Ok(ResponseDTO<object>.Success(data: resultStatus.Data));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateUpdateDTO categorydata)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.ValidationError));
            }

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            ResultStatus result = await mediator.Send(new AddCategoryCommand { userid = userId, categoryName = categorydata.Name });

            if (!result.Status)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.UnexpectedError));
            }

            return Ok(ResponseDTO<object>.Success(data: result.Message));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryCreateUpdateDTO categorydata)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.ValidationError));
            }

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            ResultStatus result = await mediator.Send(new AddCategoryCommand
            {
                userid = userId,
                categoryName = categorydata.Name
            });

            if (!result.Status)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.UnexpectedError));
            }

            return Ok(ResponseDTO<object>.Success(data: result.Message));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
             await mediator.Send(new RemoveCategoryWithProductsOrchestrator { CategoryId = id });

           

            return Ok(ResponseDTO<object>.Success(data: "Category Deleted"));
        }
    }
}
