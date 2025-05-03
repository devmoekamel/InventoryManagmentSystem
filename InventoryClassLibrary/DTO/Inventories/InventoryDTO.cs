using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.DTO.Inventories
{
    public class InventoryDTO
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Stock { get; set; }

    }
}
