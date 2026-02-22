using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagmentSystem.Core.Models
{
    public class Product: BaseModel
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        public ICollection<Inventory> Inventories { get; set; }

        public ICollection<InventoryTransaction> Transactions { get; set; }
       
    }

}
