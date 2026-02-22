using AutoMapper;
using InventoryManagmentSystem.Core.Models;

namespace InventoryManagmentSystem.Core.DTO.Warehouses
{
    public class WarehouseProfileL:Profile
    {
        public WarehouseProfileL()
        {
            CreateMap<Warehouse,WarehouseDTO>();

        }
    }
}
