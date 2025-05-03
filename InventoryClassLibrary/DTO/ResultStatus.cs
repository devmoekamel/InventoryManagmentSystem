using InventoryClassLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.DTO
{
    public class ResultStatus
    {
        public bool Status { get; set; } 
        public ErrorCode ErrorCode { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; } 
    }
}
