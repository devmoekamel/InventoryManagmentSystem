using Microsoft.AspNetCore.Identity;

namespace InventoryManagmentSystem.Core.Models
{
    public class ApplicationUser:IdentityUser
    {

        public ICollection<Warehouse> Warehouses { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
