using InventoryManagmentSystem.Core.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace InventoryManagmentSystem.Middlewares
{
    public class TransactionMiddleware : IMiddleware
    {
        InventoryContext _dbContext;
        ILogger<TransactionMiddleware> _logger;


        public TransactionMiddleware(InventoryContext context,
            ILogger<TransactionMiddleware> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            var HttpMethod = httpContext.Request.Method;
            //if (HttpMethod == "PUT" || HttpMethod == "Delete" || HttpMethod == "POST")
            //{
                await next(httpContext);
                return;
            //}

            IDbContextTransaction transaction = null;

            try
            {
                transaction = await _dbContext.Database.BeginTransactionAsync();

                next(httpContext);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                _logger.LogError("");

                throw;
            }
        }
    }
}
