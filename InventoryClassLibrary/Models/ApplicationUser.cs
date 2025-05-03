using InventoryClassLibrary.Models;
using Microsoft.AspNetCore.Identity;

namespace InventoryClassLibrary.Models
{
    public class ApplicationUser:IdentityUser
    {

        public ICollection<Warehouse> Warehouses { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
