using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Core.DTO.Auth
{
    public class LoginResult:ResultStatus
    {
        public DateTime ExpireDate { get; set; }
    }
}
