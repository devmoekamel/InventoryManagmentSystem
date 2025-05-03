using MediatR;

namespace InventoryManagmentSystem.Features.Categories.Orchestrators.Requests
{
    public class RemoveCategoryWithProductsOrchestrator : IRequest
    {
        public int CategoryId;
    }
}
