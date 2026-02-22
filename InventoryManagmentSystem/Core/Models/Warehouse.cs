    namespace InventoryManagmentSystem.Core.Models
    {
        public class Warehouse : BaseModel
        {
            public String Name { get; set; }

            public ICollection<Inventory> Inventories { get; set; }
      

    }
}
