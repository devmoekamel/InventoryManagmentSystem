using InventoryManagmentSystem.Core.Enums;

namespace InventoryManagmentSystem.Core.DTO.Reports
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
