using InventoryManagmentSystem.Shared.Models;

namespace InventoryManagmentSystem.EndPoints;

public abstract class EndpointDefinition
{
    public abstract void RegisterEndpoints(IEndpointRouteBuilder app);
    
    protected static IResult Response<T>(RequestResult<T> result)
    {
        return Results.Ok((EndPointResponse<T>)result);
    }
    
    protected static IResult Response<T>(PagedResult<T> result)
    {
        return Results.Ok(EndPointResponse<PagedResult<T>>.Success(result));
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
