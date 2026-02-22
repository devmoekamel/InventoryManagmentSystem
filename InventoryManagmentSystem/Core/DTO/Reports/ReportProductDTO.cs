using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Core.DTO.Reports
{
    public class ReportProductDTO
    {
        public string Product {  get; set; }
        public string Category { get; set; }
        public string Warehouse { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
