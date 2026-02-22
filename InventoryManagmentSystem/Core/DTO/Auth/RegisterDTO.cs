using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Core.DTO.Auth
{
    public class RegisterDTO
    {
        public String Email { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
    }
}
