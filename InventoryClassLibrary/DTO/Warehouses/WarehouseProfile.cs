using AutoMapper;
using InventoryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.DTO.Warehouses
{
    public class WarehouseProfileL:Profile
    {
        public WarehouseProfileL()
        {
            CreateMap<Warehouse,WarehouseDTO>();

        }
    }
}
