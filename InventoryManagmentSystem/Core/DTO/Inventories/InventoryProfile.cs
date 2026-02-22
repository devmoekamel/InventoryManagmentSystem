using AutoMapper;
using InventoryManagmentSystem.Core.Models;

namespace InventoryManagmentSystem.Core.DTO.Inventories
{
    public class InventoryProfile :Profile
    {

        public InventoryProfile() {

            CreateMap<Inventory, ProductInventoryDTO>()
            .ForMember(des => des.ProductName, src => src.MapFrom(i => i.Product.Name))
            .ForMember(des => des.WarehouseName, src => src.MapFrom(i => i.Warehouse.Name))
            .ForMember(des => des.Price, src => src.MapFrom(i => i.Product.Price))
            .ForMember(des => des.IsLow, src => src.MapFrom(i => i.Stock < i.Product.LowStockThreshold))
            .ForMember(des => des.WarehouseProductStock, 
             src => src.MapFrom(i => i.Stock));

            CreateMap<Inventory, InventoryDTO>();
            CreateMap<InventoryDTO, Inventory>();







        }

    }
}
