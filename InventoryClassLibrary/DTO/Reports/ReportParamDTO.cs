using Azure.Core;
using InventoryClassLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.DTO.Reports
{
    public class ReportParamDTO
    {
        public DateTime? fromDate {  get; set; }
        public DateTime?  ToDate { get; set; }
        public  int? categoryId { get; set; }
        public int? ProductId { get; set; } 
        public TransactionType? transactionType { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;

    }
}
