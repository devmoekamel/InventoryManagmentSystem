using InventoryClassLibrary.BackgroundJobs.Interfaces;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.BackgroundJobs.Concrete
{
    public class ProductNotifier : IProductNotifier
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<LowStockLog> stockLog;

        public ProductNotifier(IGenericRepository<Product> productRepo ,IGenericRepository<LowStockLog> StockLog)
        {
            this.productRepo = productRepo;
            stockLog = StockLog;
        }
        public async Task CheckProductStock()
        {
         var products =  productRepo.GetAll().
                Where(p => p.Quantity < p.LowStockThreshold)
                .ToList();

           foreach(var product in products) 
            {
                stockLog.Add(new LowStockLog
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    LowStockThreshold = product.LowStockThreshold,
                    CurrentQuantity = product.Quantity
                });
            }

           await stockLog.SaveChangesAsync();
        }
    }
}
