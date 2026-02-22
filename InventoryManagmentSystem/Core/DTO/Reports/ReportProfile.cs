using AutoMapper;
using InventoryManagmentSystem.Core.Enums;
using InventoryManagmentSystem.Core.Models;

namespace InventoryManagmentSystem.Core.DTO.Reports
{
    public class ReportProfile :Profile
    {
        public ReportProfile()
        {
            CreateMap<InventoryTransaction, TransactionReportDTO>()
             .ForMember(des => des.FromWarehouse, src => src.MapFrom(i => i.FromWarehouse.Name))
             .ForMember(des => des.ToWarehouse, src => src.MapFrom(i => i.ToWarehouse.Name))
             .ForMember(des => des.Product, src => src.MapFrom(i => i.Product.Name))
             .ForMember(des => des.Category, src => src.MapFrom(i => i.Product.Category.Name))
             .ForMember(des => des.TransactionType, src => src.MapFrom(i => i.TransactionType == TransactionType.Increase ? "Increase" :
                                                               i.TransactionType == TransactionType.Decrease ? "Decrease" :
                                                               i.TransactionType == TransactionType.Transfer ? "Transfer" : "Unknown"));


            CreateMap<Inventory, ReportProductDTO>()
             .ForMember(des => des.Product, src => src.MapFrom(i => i.Product.Name))
             .ForMember(des => des.Category, src => src.MapFrom(i => i.Product.Category.Name))
             .ForMember(des => des.Stock, src => src.MapFrom(i => i.Product.Category.Name))
             .ForMember(des => des.CreatedAt, src => src.MapFrom(i => i.CreatedAt));


        }
    }
}
