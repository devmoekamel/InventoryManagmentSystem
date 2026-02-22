using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Core.Enums
{
    public enum ErrorCode
    {
        None = 0,
        ValidationError = 1,
        UnexpectedError = 2,
        NotFound = 100,
        AlreadyExists = 101,
        InsufficientProductStock = 400,
        ProductNotExist = 500,
        WarehouseNotExist = 501
    }

}
