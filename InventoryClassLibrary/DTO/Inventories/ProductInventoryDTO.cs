using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.DTO.Inventories
{
    public class ProductInventoryDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int WarehouseProductStock { get; set; }
        public double Price { get; set; }
        public bool IsLow { get; set; }
        public string WarehouseName { get; set; }  
        

    }
}
