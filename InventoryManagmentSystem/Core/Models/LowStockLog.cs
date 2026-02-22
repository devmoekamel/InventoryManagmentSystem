using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Core.Models
{
    public class LowStockLog :BaseModel
    {
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public int CurrentQuantity { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
