using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagmentSystem.Core.Models
{
    public class Inventory : BaseModel
    {

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey(nameof(Warehouse))]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }   
        public int Stock { get; set; } 
    }
}
