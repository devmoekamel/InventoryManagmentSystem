using InventoryManagmentSystem.Core.DTO;
using InventoryManagmentSystem.Features.Categories.Commands.Requests;
using InventoryManagmentSystem.Features.Categories.Orchestrators.Requests;
using InventoryManagmentSystem.Features.Products.Commands.Requests;
using InventoryManagmentSystem.Features.Products.Queries.Requests   ;
using MediatR;

namespace InventoryManagmentSystem.Features.Categories.Orchestrators.Handlers
{
    public class RemoveCategoryWithProductsOrchestratorHandler : IRequestHandler<RemoveCategoryWithProductsOrchestrator>
    {
        private readonly IMediator mediator;

        public RemoveCategoryWithProductsOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task Handle(RemoveCategoryWithProductsOrchestrator request, CancellationToken cancellationToken)
        {

            ResultStatus Categoryresult = await mediator.Send(new DeleteCategoryCommand
            {
                categoryId = request.CategoryId,
            });

            var products = await mediator.Send(new GetAllProductsByCategoryQuery
            {
                CategoryId = request.CategoryId
            });

            foreach (var product in products)
            {

                await mediator.Send(new DeleteProductCommad
                {
                    ProductId = product.Id
                });
            }


        }
    }
}
