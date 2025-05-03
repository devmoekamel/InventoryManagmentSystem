
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Repos;

namespace WebApplication1.Middlewares
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        ILogger<GlobalErrorHandlerMiddleware> _logger;

        public GlobalErrorHandlerMiddleware(ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);


              //  await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}