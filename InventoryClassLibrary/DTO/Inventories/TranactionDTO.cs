using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.DTO.Inventories
{
    public class TranactionDTO
    {
        public int ProductId { get; set; }
        public int FromWarehouseId { get; set; }
        public int TOWarehouseId { get; set; }
        public int Stock { get; set; }
    }
}
