using InventoryManagmentSystem.Core.Enums;

namespace InventoryManagmentSystem.Core.DTO
{
    public class ResultStatus
    {
        public bool Status { get; set; } 
        public ErrorCode ErrorCode { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; } 
    }
}
