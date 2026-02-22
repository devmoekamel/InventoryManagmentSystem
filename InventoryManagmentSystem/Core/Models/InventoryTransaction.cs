using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagmentSystem.Core.Enums;

namespace InventoryManagmentSystem.Core.Models
{
    public class InventoryTransaction:BaseModel
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        
        public int? FromWarehouseId { get; set; }
        [ForeignKey(nameof(FromWarehouseId))]
        public Warehouse FromWarehouse { get; set; }

        public int? ToWarehouseId { get; set; }
        [ForeignKey(nameof(ToWarehouseId))]
        public Warehouse ToWarehouse { get; set; }
        public int Stock { get; set; }
        public bool IsArchived { get; set; } = false;

        public TransactionType TransactionType { get; set; }
    }
}
