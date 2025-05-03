namespace InventoryManagmentSystem.DTO.Products
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        
        public int LowStockThreshold { get; set; }
    }
}
