using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.DTO.Reports
{
    public class TransactionReportDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string FromWarehouse { get; set; }
        public string ToWarehouse { get; set; }
        public string Product { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public string TransactionType { get; set; }

    }
}
